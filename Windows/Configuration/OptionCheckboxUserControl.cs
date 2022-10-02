namespace Shirehorse.Core.Configuration
{
    public partial class OptionCheckboxUserControl : UserControl, IOptionUserControl
    {
        public Option Option { get; set; }
        public OptionCheckboxUserControl()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            checkBox.Text = Option.Description;
            checkBox.CheckState = Option.Bool 
                ? CheckState.Checked 
                : CheckState.Unchecked;

            if (Option.TryGetTooltip(out string tt))
                toolTip.SetToolTip(checkBox, tt);

            checkBox.CheckedChanged += CheckBox_CheckedChanged;

            Option.ValueChanged += Option_ValueChanged;
        }

        private void Option_ValueChanged(object? sender, EventArgs e)
        {
            checkBox.CheckState = Option.Bool
                ? CheckState.Checked
                : CheckState.Unchecked;
        }

        private void CheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            Option.Bool = checkBox.Checked;
        }
    }
}
