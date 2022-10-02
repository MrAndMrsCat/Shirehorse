namespace Shirehorse.Core.FiniteStateMachines
{
    public static class StaticFiniteStateMachines
    {
        public static List<IStateMachine> All { get; } = new();

        internal static void Add(IStateMachine fsm)
        {
            All.Add(fsm);

            StateMachineAdded?.Invoke(new object(), new EventArgs());
        }

        public static event EventHandler? StateMachineAdded;
    }
}
