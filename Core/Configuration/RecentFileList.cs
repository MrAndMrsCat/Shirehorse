namespace Shirehorse.Core.Configuration
{
    public class RecentList
    {
        public int Length = 10;
        public string Name;
        public string ApplicationDirectoryName;
        public bool Sorted;
        public string[] Items 
        {
            // expose array to force client to use 'Add' method
            get 
            {
                if (Sorted) items.Sort();
                return items.ToArray(); 
            } 
        } 

        protected List<string> items = new List<string>();

        private readonly Config _config;
        

        public RecentList(string applicationDirectoryName, string name)
        {
            ApplicationDirectoryName = applicationDirectoryName;
            Name = name;

            _config = new Config()
            {
                Name = name,
                ApplicationName = applicationDirectoryName,
                AutoSave = false,
                FlushTime = 10000,
                FlexibleList = true,
            };
            Load();
        }

        public void Load()
        {
            _config.Load();

            items.Clear();

            foreach (var opt in _config.Options)
                items.Add(opt.Value.ToString());
           
        }

        public virtual void Add(string str)
        {
            AddPrivate(str);
            Save();
        }

        public void AddRange(IEnumerable<string> strings)
        {
            foreach (string str in strings) items.Add(str);
            Save();
        }

        private void AddPrivate(string str)
        {
            if (str == null || str == "") return;

            items.Remove(str); // always remove
            items.Insert(0, str); // and push to top of list

            while (items.Count > Length) items.RemoveAt(Length);
        }

        public void Remove(string removeString) => RemoveRange(new List<string>() { removeString });

        public void RemoveRange(IEnumerable<string> strings)
        {
            foreach (string str in strings) items.Remove(str);

            Save();
        }

        private void Save()
        {
            _config.Options.Clear();

            foreach (string item in items)
                _config.Options.Add(new Option() { Value = item });

            _config.Save();
        }


        public void Clear() => items.Clear();

    }

    public class RecentFileList : RecentList
    {
        public bool ExcludeRemoteFiles { get; set; } = true; // this can cause large delays if the remote resource is not available

        private const string RecentFileString = "Recent Files";

        public RecentFileList(string applicationName) : base(applicationName, RecentFileString) { }

        public RecentFileList(string applicationName, string name) : base(applicationName, name) { }

        public override void Add(string filepath)
        {
            ClearMissing();

            base.Add(filepath);
        }

        public void ClearMissing()
        {
            bool remove;

            foreach (string file in items.ToArray())
            {
                remove = ExcludeRemoteFiles && file.Contains(@"\\");

                remove = remove || !(File.Exists(file) || Directory.Exists(file));

                if (remove) items.Remove(file);
            }
        }
    }
}
