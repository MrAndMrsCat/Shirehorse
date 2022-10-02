namespace Shirehorse.Core.Configuration
{
    public static class ConfigurationUserControlBindings
    {
        public static void Bind(this CheckBox checkBox, Option option)
        {
            checkBox.Checked = option.Bool;

            option.ValueChanged += (s, e) =>
            {
                checkBox.Checked = option.Bool;
            };

            checkBox.CheckedChanged += (s, e) =>
            {
                option.Bool = checkBox.Checked;
            };
        }

        public static void Bind(this ToolStripButton toolStripButton, Option option)
        {
            toolStripButton.Checked = option.Bool;

            option.ValueChanged += (s, e) =>
            {
                toolStripButton.Checked = option.Bool;
            };

            toolStripButton.CheckedChanged += (s, e) =>
            {
                option.Bool = toolStripButton.Checked;
            };
        }
    }
}
