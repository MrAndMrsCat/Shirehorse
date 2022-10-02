namespace Shirehorse.Core.Configuration
{
    public partial class ConfigurationForm : Form
    {
        public ConfigurationForm(Config config)
        {
            InitializeComponent();
            ConfigPanel.Configuration = config;
            ConfigPanel.Initialize();
            Show();
        }
    }
}
