using Shirehorse.Core.Diagnostics;
using Shirehorse.Core.Diagnostics.Logging;
using static Shirehorse.Core.FiniteStateMachines.IStateMachine;
using System.ComponentModel;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Shirehorse.Core.FiniteStateMachines
{
    public sealed class FiniteStateMachine<TStates> : IStateMachine where TStates : Enum
    {
        public FiniteStateMachine()
        {
            StaticFiniteStateMachines.Add(this);

            LogSourceName = typeof(TStates).ToString().Split('+').Last().Trim('\"');
            Logger = new(LogSourceName, ILogging.Category.StateMachine);

            Log("Built state machine");
            CurrentState = ResetState;

            _stateChangeWorker = new();
            _stateChangeWorker.DoWork += StateChangeWorker_DoWork;
        }

        private class QueuedStateChange
        {
            public QueuedStateChange(TStates newState, object? parameter, bool force)
            {
                NewState = newState;
                Parameter = parameter;
                Force = force;
            }

            public TStates NewState { get; private set; }
            public object? Parameter { get; private set;}
            public bool Force { get; private set; }
        }

        public bool Asynchronous { get; set; } = true;
        public LogLevel LoggingLevel { get; set; } = LogLevel.StateChange;
        public TStates CurrentState { get; private set; }
        public string CurrentStateName => CurrentState.ToString();
        public string[] AllStateNames { get; } = Enum.GetNames(typeof(TStates));
        public Action ResetAction { get; set; } = DefaultResetAction;
        public bool PollingPaused { get; set; }
        public bool ImplicitPollingCompleteTransition { get; set; } = true;
        public LogSource? Logger { get; set; }

        private TStates[] AllStates { get; } = Enum.GetValues(typeof(TStates)).Cast<TStates>().ToArray();
        private TStates ResetState => AllStates.First();


        private readonly Dictionary<TStates, StateAction> StateActions = new();

        private readonly Dictionary<(TStates EntryState, TStates ExitState), Transition> Transitions = new();

        public string LogSourceName { get; }

        private void Log(string message) => Logger?.Log(ILogging.Category.StateMachine, message);

        

        private System.Timers.Timer PollingTimer
        {
            // lazy initialize
            get
            {
                if (_pollingTimer is null)
                {
                    _pollingTimer = new();
                    _pollingTimer.Elapsed += PollingTimer_Elapsed;
                }
                return _pollingTimer;
            }
        }
        private System.Timers.Timer? _pollingTimer;

        public string PollingCompleteState => StateActions.TryGetValue(CurrentState, out StateAction? action) 
            ? $"{action.PollingCompleteState}" 
            : "";

        public double PollingInterval => PollingTimer.Interval;

        

        public string[] EntryStateNames(string exitStateName) => Transitions
            .Keys
            .Where(x => x.ExitState.Equals(Parse(exitStateName)))
            .Select(x => x.EntryState.ToString())
            .ToArray();

        public string[] ExitStateNames(string entryStateName) => Transitions
            .Keys
            .Where(x => x.EntryState.Equals(Parse(entryStateName)))
            .Select(x => x.ExitState.ToString())
            .ToArray(); 

        private TStates Parse(string name) => (TStates)Enum.Parse(typeof(TStates), name);

        private static void DefaultResetAction() { }

        

        public void ChangeState(TStates newState) => ChangeState(newState, null);
        public void ChangeState(TStates newState, object? parameter) => ChangeState(newState, parameter, false);

        private readonly BackgroundWorker _stateChangeWorker;
        private readonly ConcurrentQueue<(TStates newState, object? parameter, bool force, StackFrame[] stack)> _stateChangeQueue = new();
        public void ChangeState(TStates newState, object? parameter = null, bool force = false)
        {
            if (Asynchronous)
            {
#if DEBUG
                _stateChangeQueue.Enqueue((newState, parameter, force, new StackTrace().GetFrames()));
#else
                _stateChangeQueue.Enqueue((newState, parameter, force, Array.Empty<StackFrame>()));
#endif


                if (_stateChangeWorker.IsBusy)
                {
                    Logger?.Log(ILogging.Category.Debug, $"{_stateChangeQueue.Count} state changes queued");
                }
                else
                {
                    _stateChangeWorker.RunWorkerAsync();
                }
            }
            else
            {
                ChangeStatePrivate(newState, parameter, force);
            }
        }

        private void StateChangeWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            while(!_stateChangeQueue.IsEmpty)
            {
                if (_stateChangeQueue.TryDequeue(out var arg))
                {
                    StateChangeCallerStack = arg.stack;

#if DEBUG
                    try
                    {
                        //TaskWithTimeout.Invoke(360000, () =>
                        //{
                            ChangeStatePrivate(arg.newState, arg.parameter, arg.force);
                        //});
                    }
                    catch (Exception ex)
                    {
                        ex.Data.Add("StateChangeStack:  ", "State change call stack");

                        for (int i = 0; i < StateChangeCallerStack.Length; i++)
                        {
                            ex.Data.Add($"StateChangeStack{i,3}", StateChangeCallerStack[i]
                                .ToString()
                                .Replace("\n", "")
                                .Replace("\r", ""));
                        }

                        SystemHandler.Handle(ex);
                    }


#else
                    ChangeStatePrivate(arg.newState, arg.parameter, arg.force);
#endif
                }
            }
        }

        private StackFrame[]? StateChangeCallerStack { get; set; }

        public void ChangeStatePrivate(TStates newState, object? parameter = null, bool force = false)
        {
            // Do nothing if the state is the same
            if (CurrentState.Equals(newState))
            {
                Log($"{newState} - already set, no actions performed");
                return;
            }

            if (newState.Equals(ResetState))
            {
                Reset();
                ConfigurePolling();
                return;
            }

            if (force)
            {
                Log($"{newState} - forced, no actions performed");
                CurrentState = newState;
                ConfigurePolling();
                return;
            }

            if (Transitions.TryGetValue((CurrentState, newState), out Transition? transition) || (PollingTimer.Enabled && ImplicitPollingCompleteTransition))
            {
                if (transition is null) // implicit
                {
                    transition = Transitions[(CurrentState, newState)] = new Transition(() => { }); // add no action
                }

                var oldState = CurrentState;

                PollingTimer.Stop();
                try
                {
                    // exit action
                    if (StateActions.TryGetValue(CurrentState, out StateAction? action) && action.ExitAction != null)
                    {
                        if (LoggingLevel.HasFlag(LogLevel.PerformStateAction)) Log($"Performing {CurrentState} exit action");
                        action.ExitAction();
                    }

                    // it is possible following actions could trigger another state change, so ensure we set the new state now
                    CurrentState = newState;

//#if DEBUG
//                    Log($"{CurrentState} - internally changed");
//#endif

                    // transition
                    if (transition.Action is not null)
                    {
                        if (LoggingLevel.HasFlag(LogLevel.PerformTransition)) Log($"Performing transition from {oldState} to {newState}");

                        transition.Action();
                    }
                    else if (transition.ActionWithParameter is not null)
                    {
                        if (parameter is not null)
                        {
                            if (LoggingLevel.HasFlag(LogLevel.PerformTransition)) Log($"Performing transition from {oldState} to {newState} with parameter: {parameter}");

                            transition.ActionWithParameter(parameter);
                        }
                        else throw new ArgumentException($"Transition for: {oldState} to: {newState} requires parameter(s)");
                    }

                    // maybe put this here for a reason? probably for syncronous operation logging consistancy
                    //OnStateChanged();

                    // entry action
                    if (StateActions.TryGetValue(CurrentState, out action))
                    {
                        if (action.EntryAction is not null)
                        {
                            if (LoggingLevel.HasFlag(LogLevel.PerformStateAction)) Log($"Performing {CurrentState} entry action ");

                            action.EntryAction();
                        }
                        else if (parameter is not null)
                        {
                            if (action.EntryActionWithParameter is not null)
                            {
                                if (LoggingLevel.HasFlag(LogLevel.PerformTransition)) Log($"Performing {CurrentState} entry action with parameter: {parameter}");

                                action.EntryActionWithParameter(parameter);
                            }
                            else throw new ArgumentException($"Entry action for {CurrentState} does accept parameters");
                        }
                    }

                    OnStateChanged();
                }
                catch (Exception ex)
                {
                    Log($"Exception during {oldState} to {newState} tranisition");
                    Reset();

                    SystemHandler.Handle(ex);
                }
                finally
                {
                    ConfigurePolling();
                }
            }
            else
            {
                Log($"Transition from {CurrentState} to {newState} not permitted, no actions performed");
            }
        }
        public void ChangeStateByName(string newStateName, bool force) => ChangeState(Parse(newStateName), force: force);

        public void Reset()
        {
            //Log("State machine reset"); // already logged OnStateChanged()
            CurrentState = ResetState;
            ResetAction();
            OnStateChanged();
        }

        private void ConfigurePolling()
        {
            if (StateActions.TryGetValue(CurrentState, out StateAction? action))
            {
                if (action.PollingFunction != null && action.PollingInterval != 0)
                {
                    PollingTimer.Interval = action.PollingInterval;
                    PollingTimer.Start();

                    if (LoggingLevel.HasFlag(LogLevel.Polling)) Log($"{CurrentState} polling at {action.PollingInterval}ms");
                }
            }
        }


        public event EventHandler? StateChangedNamed;
        public event EventHandler<FSMEventArgs>? StateChanged;
        private void OnStateChanged() 
        {
            if (LoggingLevel.HasFlag(LogLevel.StateChange)) Log($"{CurrentState}");

            StateChanged?.Invoke(this, new FSMEventArgs(CurrentState));
            StateChangedNamed?.Invoke(this, EventArgs.Empty);
        } 


        public class FSMEventArgs : EventArgs
        {
            public FSMEventArgs(TStates newState) => NewState = newState;
            public TStates NewState;
        }

        private void PollingTimer_Elapsed(object? sender, EventArgs e)
        {
            if (!PollingPaused)
            {
                try
                {
                    if (StateActions.TryGetValue(CurrentState, out StateAction? action) && action.PollingFunction is not null && action.PollingCompleteState is not null)
                    {
                        if (action.PollingFunction()) ChangeState(action.PollingCompleteState);
                    }                    
                }
                catch
                {
                    Reset();
                    throw;
                }
            }
        }


        public StateAction this[TStates key]
        {
            //get => StateActions[key];
            get
            {
                return StateActions.TryGetValue(key, out StateAction? value)
                    ? value
                    : StateActions[key] = new();
            }
            set => StateActions[key] = value;
        }
        public class StateAction
        {
            /// <summary>
            /// On PollingInterval timer elapsed perform the polling function; 
            /// if return value is true change to PollingCompleteState, if false resume polling
            /// </summary>
            public StateAction(Func<bool> pollingFunction, int pollingInterval, TStates completeState)
            {
                PollingFunction = pollingFunction;
                PollingInterval = pollingInterval;
                PollingCompleteState = completeState;
            }

            /// <summary>
            /// On timeout elapsed (Polling Interval) change state to timeooutState (PollingCompleteState)
            /// </summary>
            /// <param name="timeout"> Timeout (ms). </param>
            /// <param name="timeoutState"> When timeout elapses, go to this state. </param>
            public StateAction(int timeout, TStates timeoutState) 
            {
                PollingFunction = new Func<bool>(() => { return true; });
                PollingInterval = timeout;
                PollingCompleteState = timeoutState;
            }

            /// <summary>
            /// Default contructor, no polling actions
            /// </summary>
            public StateAction() {  }

            public Func<bool>? PollingFunction { get; set; }
            public int PollingInterval { get; set; }
            public TStates? PollingCompleteState { get; set; }
            public Action? EntryAction { get; set; }
            public Action<object>? EntryActionWithParameter { get; set; }
            public Action? ExitAction { get; set; }
            //public Action<object>? ExitActionWithParameter { get; set; } // doubt this would be useful?
        }


        public Transition this[TStates entryState, TStates exitState]
        {
            get => Transitions[(entryState, exitState)];
            set => Transitions[(entryState, exitState)] = value; 
        }
        public class Transition
        {
            public Transition() { }

            public Action? Action { get; private set; }
            public Transition(Action action) => Action = action;
            

            public Action<object>? ActionWithParameter { get; private set; }
            public Transition(Action<object> action) => ActionWithParameter = action;
        }

        public override string ToString()
        {
            string result = GetType().ToString();

            var split = result.Split('[');

            if (split.Length == 2)
            {
                result = split[1].Replace('+', '<').Replace(']', '>');
            }

            return ToStringPrefix + result;
        }

    }
}
