namespace Shirehorse.Core.FiniteStateMachines
{
    public interface IFiniteStateMachineUserInterface
    {
        public void Show();
        public void Hide();
        public void Initialize(IStateMachine fsm);
        public bool Initialized { get; }
    }
}
