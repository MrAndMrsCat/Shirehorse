namespace Shirehorse.Core.Configuration
{
    public class PersistantDictionary
    {
        public PersistantDictionary(string applicationName, string name)
        {
            ApplicationName = applicationName;
            Name = name;

            ConfigFile = new Config()
            {
                Name = name,
                ApplicationName = applicationName,
                AutoSave = false,
                FlexibleList = true,
            };
            Load();
        }

        public string Name;
        public string ApplicationName;
        public char Delimiter = '\t';

        protected Dictionary<string, string> CachedDictionary = new();

        private readonly Config ConfigFile;

        public string this[string key]
        {
            get => CachedDictionary[key];
            set
            {
                CachedDictionary[key] = value;

                Save();
            }
        }

        public IEnumerable<string> Keys => CachedDictionary.Keys;

        public void Load()
        {
            try
            {
                ConfigFile.Load();

                CachedDictionary.Clear();

                foreach (var opt in ConfigFile.Options)
                {
                    var kv = opt.Value.ToString().Split(Delimiter);
                    CachedDictionary[kv[0]] = kv[1];
                }

            }
            catch { }
        }

        private void Save()
        {
            ConfigFile.Options.Clear();

            foreach (var kv in CachedDictionary)
            {
                ConfigFile.Options.Add(new Option() { Value = kv.Key + Delimiter + kv.Value });
            }

            ConfigFile.Save();
        }



        public void Remove(string key)
        {
            CachedDictionary.Remove(key);

            Save();
        }

        public bool TryGetValue(string key, out string value) => CachedDictionary.TryGetValue(key, out value);

        public string ValueOrDefault(string key)
        {
            if (TryGetValue(key, out string value)) return value;
            else return "";
        }
    }
}
