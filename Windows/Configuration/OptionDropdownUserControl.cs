namespace Shirehorse.Core.Configuration
{
    public partial class OptionControl_Dropdown : UserControl, IOptionUserControl
    {
        public Option Option { get; set; }
        public OptionControl_Dropdown()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            lbl_description.Text = Option.Description;

            if (Option.TryGetTooltip(out string tt))
                toolTip.SetToolTip(comboBox, tt);

            comboBox.DropDownStyle = Option.UserCanAddChoice 
                ? ComboBoxStyle.DropDown 
                : ComboBoxStyle.DropDownList;

            var choices = DefaultChoices();
            if (Option.Choices != null) choices = Option.Choices;

            foreach (object choice in choices) 
                comboBox.Items.Add(choice); 

            if (comboBox.Items.IndexOf(Option.Value) < 0) 
                comboBox.Items.Add(Option.Value); 
            comboBox.SelectedItem = Option.Value;

            if (Option.UserControlWidth != 0)
            {
                comboBox.Width = Option.UserControlWidth;
                lbl_description.Location = new Point { X = Option.UserControlWidth + 10, Y = 7 } ;
            }

            comboBox.SelectedValueChanged += ComboBox_SelectedValueChanged;



            Option.ValueChanged += Option_ValueChanged;
        }

        private void Option_ValueChanged(object sender, EventArgs e)
        {
            var ev = (Option.ValueChangedEventArgs)e;
            comboBox.Text = ev.Value.ToString();
        }

        private void ComboBox_SelectedValueChanged(object? sender, EventArgs e)
        {
            switch (Option.Value)
            {
                default:
                    if (comboBox.SelectedItem is null)
                    {
                        Option.Value = comboBox.Text;
                    }
                    else
                    {
                        Option.Value = comboBox.SelectedItem;
                    }

                    break;

                case int _:
                    int value;
                    if (int.TryParse(comboBox.Text, out value))
                    {
                        comboBox.BackColor = Color.White;
                        Option.Value =value;
                    }
                    else { comboBox.BackColor = Color.Salmon; }
                    break;
            }
        }


        private List<object> DefaultChoices()
        {
            switch (Option.Value)
            {
                case bool _:
                    return new List<object>()
                    {
                        true,
                        false
                    };

                default:
                    return new List<object>();
            }
        }

        private void ComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ComboBox_SelectedValueChanged(this, EventArgs.Empty);
                e.Handled = true;
            }
        }
    }
}
