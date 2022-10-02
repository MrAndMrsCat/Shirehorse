using Shirehorse.Core.Diagnostics;
using Shirehorse.Core.Extensions;

namespace Shirehorse.Core.FiniteStateMachines
{
    public partial class FiniteStateMachineUserControl : UserControl, IFiniteStateMachineUserInterface
    {        
        public FiniteStateMachineUserControl() => InitializeComponent();

        public bool Initialized
        {
            get => _initialized && StateMachine is not null && !IsDisposed;
            set => _initialized = value;
        }
        private bool _initialized;

        private IStateMachine? StateMachine { get; set; }

        private readonly int StateColumnIndex = 0;
        private readonly Color ActiveStateColor = Color.LightGreen;

        public void Initialize(IStateMachine fsm)
        {
            StateMachine = fsm;

            StatesGrid.Columns.Add("State", "State");
            grid_entryStates.Columns.Add("EntryState", "Entry States");
            grid_exitStates.Columns.Add("ExitState", "Exit States");

            PopulateStates();

            // why did I do this? doesn't look like a good solution 
            Task.Delay(50)
                .GetAwaiter()
                .OnCompleted(() => 
                {
                    PopulateTransitions(StateMachine.CurrentStateName);
                    StatesGrid.ClearSelection();
                });

            checkBox_pollingPaused.DataBindings.Add("Checked", StateMachine, "PollingPaused");

            StateMachine.StateChangedNamed += (s,e) => this.ThreadSafe(HighlightCurrentState);  // FSM can call this
        }

        public new void Show() => this.ThreadSafe(base.Show); // FSM can call this
        public new void Hide() => this.ThreadSafe(base.Hide); // FSM can call this



        private void PopulateStates()
        {
            foreach (var val in StateMachine.AllStateNames)
                StatesGrid.Rows.Add(new object[] { val });

            HighlightCurrentState();
        }



        private void HighlightCurrentState()
        {
            try
            {
                foreach (DataGridViewRow row in StatesGrid.Rows)
                {
                    bool rowIsCurrentState = row.Cells[StateColumnIndex].Value.ToString() == StateMachine.CurrentStateName;

                    row.DefaultCellStyle.BackColor = rowIsCurrentState
                        ? ActiveStateColor
                        : SystemColors.Window;


                    if (checkBox_selectStateOnChange.Checked) PopulateTransitions(StateMachine.CurrentStateName);
                    else Button_ForceStateChange.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                SystemHandler.Handle(ex);
            }
        }

        private void PopulateTransitions(string selectedStateName)
        {
            grid_entryStates.Rows.Clear();
            foreach (var val in StateMachine.EntryStateNames(selectedStateName))
                grid_entryStates.Rows.Add(new object[] { val });

            grid_entryStates.ClearSelection();

            grid_exitStates.Rows.Clear();
            foreach (var val in StateMachine.ExitStateNames(selectedStateName))
            {
                string display = val == StateMachine.PollingCompleteState
                    ? FormatForPollingCompleteExitState(val)
                    : val;

                grid_exitStates.Rows.Add(new object[] { display });
            }
                
            if (checkBox_selectFirstTransitionOnStateChange.Checked)
            {
                button_startTransition.Enabled = grid_exitStates.Rows.Count > 0;
            }
            else
            {
                grid_exitStates.ClearSelection();
                button_startTransition.Enabled = false;
            }
           


            Button_ForceStateChange.Enabled = SelectedStateName(StatesGrid) != StateMachine.CurrentStateName;
        }

        string FormatForPollingCompleteExitState(string state) => $"{state} (Polling: {StateMachine.PollingInterval}ms)";

        private string SelectedStateName(DataGridView grid) => (grid.SelectedRows.Count == 1)
            ? grid.SelectedRows[0].Cells[StateColumnIndex].Value.ToString().Split(' ')[0] // may have appended polling info
            : "";

        private void StatesGrid_CellClick(object sender, DataGridViewCellEventArgs e) => PopulateTransitions(SelectedStateName(StatesGrid));

        private void StartTransition_Click(object sender, EventArgs e)
        {
            StateMachine?.ChangeStateByName(SelectedStateName(grid_exitStates), force: false);
            StatesGrid.ClearSelection();
            Button_ForceStateChange.Enabled = false;
        }

        private void ForceStateChange_Click(object sender, EventArgs e) 
        {
            StateMachine?.ChangeStateByName(SelectedStateName(StatesGrid), force: true);
            HighlightCurrentState();
        } 

        private void ExitStatesGrid_CellClick(object sender, DataGridViewCellEventArgs e) => button_startTransition.Enabled = true;

        private void Reset_Click(object sender, EventArgs e) => StateMachine?.Reset();
    }
}
