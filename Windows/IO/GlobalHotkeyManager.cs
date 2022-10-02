using static Shirehorse.Core.GlobalHotkey;

namespace Shirehorse.Core.Configuration
{
    public class HotkeyManager
    {
        
        public string ApplicationName;

        private static readonly string Name = "Hotkeys";

        private List<object> Actions;
        private Config config;


        public HotkeyManager(string applicationName, List<object> actions, List<Option> defaults)
        {
            ApplicationName = applicationName;
            Actions = actions;

            config = new Config()
            {
                Name = Name,
                ApplicationName = applicationName,
                AutoSave = true,
                FlexibleList = true,
                Defaults = defaults
            };
            Load();
        }

        public void Load()
        {
            config.Load();

            foreach (var opt in config.Options) 
            {
                var hk = opt.Value as GlobalHotkey;
                hk.Initialize();
                hk.HotKeyPressed += HotKey_KeyPressed;
            }
        }

        public void ConfigPanel_BindInitialize(ref ConfigPanel panel)
        {
            panel.Configuration = config;
            panel.Initialize();
        }

        private void HotKey_KeyPressed(object sender, HotKeyPressedEventArgs e) => OnHotKeyPressed(e);

        public void OnHotKeyPressed(HotKeyPressedEventArgs e) { KeyPressed?.Invoke(this, e); }
        public event EventHandler<HotKeyPressedEventArgs> KeyPressed;
    }

    public class Option_Hotkey : CustomOption
    {
        public override string Type => typeof(GlobalHotkey).ToString();
        public override object Deserialize(string data)
        {
            var fields = data.Split(';');

            return new GlobalHotkey()
            {
                Action = fields[0],
                ModKey = (HotModifierKeys)Enum.Parse(typeof(HotModifierKeys), fields[1]),
                Key = (Keys)Enum.Parse(typeof(Keys), fields[2]),
            };
        }
        public override string Serialize(object obj)
        {
            var hotkey = obj as GlobalHotkey;

            return string.Concat(
                hotkey.Action,
                ';',
                hotkey.ModKey.ToString(),
                ';',
                hotkey.Key.ToString()
                );
        }

        public override IOptionUserControl Control => new Hotkey_Control();
    }
}
