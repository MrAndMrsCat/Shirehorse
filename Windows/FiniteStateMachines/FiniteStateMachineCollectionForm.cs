using Shirehorse.Core.Extensions;

namespace Shirehorse.Core.FiniteStateMachines
{
    public partial class FiniteStateMachineCollectionForm : Form
    {
        public FiniteStateMachineCollectionForm()
        {
            InitializeComponent();
            StaticFiniteStateMachines.StateMachineAdded += (s, e) => this.ThreadSafe(ShowAll);
        }


        public void ShowAll()
        {
            foreach (IStateMachine fsm in StaticFiniteStateMachines.All)
            {
                Add(fsm);
            }
        }

        private void Add(IStateMachine fsm)
        {
            if (!FiniteStateMachines.Contains(fsm))
            {
                var fsmUserControl = new FiniteStateMachineUserControl();
                fsmUserControl.Initialize(fsm);

                treeViewNavigationUserControl.Add(fsm.ToString().Replace(IStateMachine.ToStringPrefix, ""), fsmUserControl);

                FiniteStateMachines.Add(fsm);
            }
        }

        private List<IStateMachine> FiniteStateMachines { get; } = new();

        private void FiniteStateMachineForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //Hide();
        }
    }
}
