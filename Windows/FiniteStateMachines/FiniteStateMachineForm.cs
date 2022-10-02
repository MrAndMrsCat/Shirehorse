using Shirehorse.Core.Extensions;

namespace Shirehorse.Core.FiniteStateMachines
{
    public partial class FiniteStateMachineForm : Form, IFiniteStateMachineUserInterface
    {
        public FiniteStateMachineForm() => InitializeComponent();

        public bool Initialized
        {
            get => _initialized && StateMachine is not null && !IsDisposed;
            set => _initialized = value;
        }
        private bool _initialized;

        private IStateMachine? StateMachine { get; set; }

        public void Initialize(IStateMachine fsm)
        {
            StateMachine = fsm;
            FSMUserControl.Initialize(fsm);
            Text = fsm.ToString();
            Initialized = true;
        }

        public new void Hide() => this.ThreadSafe(base.Hide); // FSM can call this
        public new void Show()
        {
            if (StateMachine is not null)
            {
                if (IsDisposed)
                {
                    InitializeComponent();
                    Initialize(StateMachine);
                }

                this.ThreadSafe(base.Show); // FSM can call this
            }
        }

        private void FiniteStateMachineForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
