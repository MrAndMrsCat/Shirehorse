[Shirehorse.Core.FiniteStateMachines]

Each state machine is a generic class, the generic must be a type of Enum, which defines all the valid states.
FiniteStateMachine<TStates> : IStateMachine where TStates : Enum

The first item in the enum is the reset (or base/ground) state, it is recommended that this first state is named Reset (e.g. MyFSMEnum.Reset)

The FSM generic has two indexers, one for state actions, one for transitions:
	StateAction this[TStates key]
	Transition this[TStates entryState, TStates exitState]

All state actions are created when the FSM is constructed, so can be immediatedly be configured like so:

    StateMachine[MyFSMStates.StartSomeWork].EntryAction = () =>
    {
        // do some work
    };

or we can overwrite e.g.

    StateMachine[MyFSMStates.OpenConnection] = new()
    {
        EntryAction = () =>
        {
            // some entry action
            TryOpenMyConnection();
        },

        PollingFunction = () =>
        {
            // poll something, returning true will kick me to the PollingCompleteState
            bool pollResult = CheckConnectionIsOpen();

            return pollResult;
        },

        PollingInterval = 1000,
        PollingCompleteState = MyFSMStates.RequestInfoOverConnection
    };

    StateMachine[MyFSMStates.RequestInfoOverConnection] = new(timeout: 5000, timeoutState: MyFSMStates.RequestTimeout)
    {
        EntryAction = () =>
        {
            // send some request
        }
    };

It is not necessary to define entry / exit / polling actions for all states, however, all valid state transitions must be explicitly defined
(except StateActions with a PollingCompleteState or TimeoutState, these tranisitions are assumed valid).

it is recommended to build all state transitions in a method for clarity e.g. 

    private void BuildFSMTransitions()
    {
        StateMachine[VPN.Reset,                  VPN.Launch] = new();
        StateMachine[VPN.Launch,                 VPN.OpenConnectionDelay] = new();
        StateMachine[VPN.OpenConnectionDelay,    VPN.OpenConnection] = new();
        StateMachine[VPN.OpenConnection,         VPN.CredentialWindowDelay] = new();
        StateMachine[VPN.CredentialWindowDelay,  VPN.EnterCredentials] = new();
        StateMachine[VPN.EnterCredentials,       VPN.HideMainWindow] = new();
    }

The state machine can be kicked down to the first state with .Reset()

All state transitions are handled asyncronously on a dedicated state machine thread, when .ChangeState(TStates newState) [or .ChangeState(TStates newState, object? parameter)] 
is invoked, this is added to the state change queue