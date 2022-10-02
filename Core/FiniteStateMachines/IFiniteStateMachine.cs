namespace Shirehorse.Core.FiniteStateMachines
{
    public interface IStateMachine
    {
        [Flags]
        public enum LogLevel
        {
            None = 0,
            StateChange = 1,
            PerformStateAction = 2,
            PerformTransition = 4,
            Polling = 8,
        }
        string CurrentStateName { get; }
        string[] AllStateNames { get; }
        string[] EntryStateNames(string exitStateName);
        string[] ExitStateNames(string entryStateName);
        string PollingCompleteState { get; }
        double PollingInterval { get; }
        

        bool PollingPaused { get; set; }
        void ChangeStateByName(string newStateName, bool force);

        void Reset();

        event EventHandler StateChangedNamed;

        public static string ToStringPrefix => "Finite state machine - ";
    }
}
