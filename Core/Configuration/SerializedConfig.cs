namespace Shirehorse.Core.Configuration
{
    [Serializable()]
    public class SerializedConfig : Option
    {
        public int Version;
        public List<Option> AllOptions = new List<Option>();

        public SerializedConfig() { }

        public SerializedConfig(string name)
        {
            Name = name;
            Type = GetType().ToString();
        }

        public void Initialize()
        {
            foreach (Option opt in AllOptions)
            {
                opt.ValueChanged += Opt_ValueChanged;
            }
        }

        public void Add(Option opt)
        {
            AllOptions.Add(opt);
            opt.ValueChanged += Opt_ValueChanged;
            Save();
        }

        public void Remove(Option opt)
        {
            AllOptions.Remove(opt);
            Save();
        }

        public Option this[string key]
        {
            get
            {
                foreach (Option opt in AllOptions)
                {
                    if (key == opt.Name) return opt;
                }
                return null;
            }
        }

        private void Save() => OnValueChanged(new ValueChangedEventArgs() { Save = true });

        private void Opt_ValueChanged(object sender, ValueChangedEventArgs e)
        {


            if (e.Save) Save();
            OnValueChanged(e);
        }

        public void Trigger_Values_Changed()
        {
            foreach (var opt in AllOptions)
                opt.OnValueChanged(new ValueChangedEventArgs() { Name = opt.Name, Value = opt.Value });
        }

    }

}

