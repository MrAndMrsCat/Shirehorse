using Shirehorse.Core.Configuration;

namespace Shirehorse.Core
{
    internal partial class Hotkey_Control : UserControl, IOptionUserControl
    {
        private GlobalHotkey HotKey;

        public Option Option { get; set; }

        public Hotkey_Control()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            HotKey = Option.Value as GlobalHotkey;

            foreach (HotModifierKeys flag in Enum.GetValues(typeof(HotModifierKeys)))
                checkedListBox.Items.Add(flag, HotKey.ModKey.HasFlag(flag));
            
            comboBox_keys.Items.AddRange(Enum.GetNames(typeof(Keys)).ToArray());
            comboBox_action.Items.AddRange(Option.Choices.ToArray());

            comboBox_action.SelectedItem = HotKey.Action;
            comboBox_keys.SelectedItem = HotKey.Key.ToString();

            checkedListBox.SelectedIndexChanged += checkedListBox_SelectedIndexChanged;
            comboBox_keys.SelectedValueChanged += comboBox_SelectedValueChanged;
            comboBox_action.SelectedValueChanged += comboBox_SelectedValueChanged;
        }

        

        private void ApplyChange()
        {
            HotModifierKeys modkeys = 0;

            foreach (var flag in checkedListBox.CheckedItems)
                modkeys |= (HotModifierKeys)flag;

            Enum.TryParse(comboBox_keys.SelectedItem.ToString(), out Keys key);
            

            if (key != Keys.None)
            {
                HotKey.Dispose();

                if (GlobalHotkey.Exists(modkeys, key))
                    BackColor = Color.Salmon;

                else
                {
                    BackColor = SystemColors.Control;

                    var action = comboBox_action.SelectedItem.ToString();
                    HotKey = new GlobalHotkey() { Action = action, ModKey = modkeys, Key = key };

                    Option.Value = HotKey;
                }
            }
        }

        private void comboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ApplyChange();
        }

        private void comboBox_SelectedValueChanged(object sender, EventArgs e) => ApplyChange();

        private void checkedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox.ClearSelected(); // just to clear the highlighting
            ApplyChange();
        }
    }


}
