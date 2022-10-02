namespace Shirehorse.Core.Configuration
{
    public partial class ConfigPanel : UserControl
    {
        public Config Configuration;

        public ConfigPanel()
        {
            InitializeComponent();
            BackColor = SystemColors.Control;
            Dock = DockStyle.Fill;
        }

        public void Initialize()
        {
            int Y_pos = 4;
            foreach (Option option in Configuration.Options)
            {
                IOptionUserControl control;

                if (CustomOption.Instances.TryGetValue(option.Type, out CustomOption customOption))
                {
                    control = customOption.Control;
                }
                else 
                {
                    switch (option.Value)
                    {
                        case bool _:
                            control = new OptionCheckboxUserControl();
                            break;

                        default:
                            control = new OptionControl_Dropdown();
                            break;
                    }

                    
                }
                

                control.Option = option;
                control.Initialize();

                var uc = control as UserControl;

                Controls.Add(uc);
                uc.Location = new Point() { Y = Y_pos };
                Y_pos += uc.Height;
            }
        }

    }


}
