using System.Xml;
using System.Xml.Serialization;
using Shirehorse.Core.Diagnostics.Logging;

namespace Shirehorse.Core.Configuration
{
    /// <summary>Class <c>Config</c> provides persistant configuration with xml under the user's appdata folder, with automatic backup and UI components </summary>
    public class Config
    {
        static Config()
        {
            // Rename properties  
            var attribute_value = new XmlElementAttribute() { ElementName = "Value" };
            var attribute_default = new XmlElementAttribute() { ElementName = "Default" };

            var attributes = new XmlAttributes();
            attributes.XmlElements.Add(attribute_value);

            var attributes_def = new XmlAttributes();
            attributes_def.XmlElements.Add(attribute_default);

            var overrides = new XmlAttributeOverrides();
            overrides.Add(typeof(Option), "Value_Serialized", attributes);
            overrides.Add(typeof(Option), "Default_Value_Serialized", attributes_def);

            Serializer = new XmlSerializer(typeof(SerializedConfig), overrides);
        }
        public Config() 
        {
            FlushTimer.Elapsed += (s, e) => { if (FlushRequired) SavePrivate(); };
        }

        public string ApplicationName { get; set; } = "";
        public string Name { get; set; } = "";
        public string FilePath => Path.Combine(ApplicationDataDirectory, $"{Name}.xml");
        public string ApplicationDataDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);

        public LogSource? Log { get; set; } = new("Configuration");

        public List<Option>? Options => _serializedConfig?.AllOptions; 
        public List<Option> Defaults = new ();
        public bool FlexibleList = false; // will not modify list on startup if the file already exists 

        /// <value>Property <c>AutoSave</c> When an option's value is changed, call Save().</value>
        public bool AutoSave = true;
        public bool AutoRestore { get; set; } = true;

        /// <value>If value > 0 delay write to filesystem.</value>
        public double FlushTime
        {
            get => FlushTimer.Interval;
            set 
            {
                FlushTimer.Enabled = value > 0;
                if (value > 0) FlushTimer.Interval = value;
            } 
        }

        private bool FlushRequired = false;
        private readonly System.Timers.Timer FlushTimer = new ();

        private readonly string BackupFileNameSuffix = ".backup";
        private static readonly XmlSerializer Serializer;

        private SerializedConfig? _serializedConfig;
        private bool isNew = false;



        public void Save()
        {
            if (FlushTimer.Enabled)
            {
                FlushRequired = true;
            }
            else
            {
                SavePrivate();
            }
        }

        private readonly object _fileAccessLock = new();

        private void SavePrivate()
        {
            if (_intialized)
            {
                Log?.Log($"Saving configuration {Name}");

                FileInfo file = new (FilePath);
                FileInfo backup = new (FilePath + BackupFileNameSuffix);
                file.Directory?.Create();

                if (file.Exists) file.CopyTo(FilePath + BackupFileNameSuffix, true);

                lock (_fileAccessLock)
                {
                    try
                    {
                        XmlWriterSettings settings = new()
                        {
                            Indent = true,
                        };

                        using var writeStream = File.CreateText(file.FullName);
                        using var writer = XmlWriter.Create(writeStream, settings);

                        Serializer.Serialize(writer, _serializedConfig);

                        writer.Flush();
                    }
                    catch (Exception ex)
                    {
                        Log?.Log(ILogging.Category.Error, $"Serialization Failure: {ex.Message}");
                        Log?.Log(ex);
                        if (backup.Exists)
                        {
                            file.Delete();
                            backup.CopyTo(FilePath);
                        }
                    }
                }


                FlushRequired = false; // always, or it will keep posting if there is a persistant issue
            }
        }

        public void Load()
        {
            _serializedConfig = Load(FilePath);

            if (_serializedConfig is null && AutoRestore)
            {
                _serializedConfig = Load(FilePath + BackupFileNameSuffix);
            }

            bool exists = _serializedConfig is not null;

            // if the above failed, create new
            _serializedConfig ??= new (Name);

            Initialize();

            if (!exists) SavePrivate();
        }


        private SerializedConfig? Load(string path)
        {
            lock (_fileAccessLock)
            {
                if (!File.Exists(path))
                {
                    Log?.Log(ILogging.Category.Warning, $"Cannot load {path}, file does not exisst");
                    return null;
                }

                try
                {
                    Log?.Log($"Loading {Name} from: {path}");

                    using StreamReader reader = File.OpenText(path);

                    return Serializer.Deserialize(reader) as SerializedConfig;
                }
                catch (Exception ex)
                {
                    Log?.Log(ILogging.Category.Error, $"Deserialization Failure: {ex.Message}");
                    Log?.Log(ex);
                }
            }

            return null;
        }

        public Option? this[string key] => _serializedConfig?[key];

        private bool _intialized;

        private void Initialize()
        {
            if (_serializedConfig is null) throw new InvalidOperationException($"{nameof(_serializedConfig)} is null, must successfully load before Initialize()");

            var updated_options = new List<Option>();
            var found_names = new List<string>();

            foreach (Option opt in Defaults) // enumerate defaults to get ordered list
            {
                if (_serializedConfig[opt.Name] is Option found)
                {
                    found.Update(opt);
                    updated_options.Add(found);
                    found_names.Add(found.Name);
                }
                else if (!FlexibleList || isNew) // insert missing (upgrade)
                {
                    found = opt;
                    updated_options.Add(found);
                    found_names.Add(found.Name);
                }
            }
            if (FlexibleList) // preserve items not defined in default list
            {
                foreach (var opt in _serializedConfig.AllOptions)
                {
                    if (!found_names.Contains(opt.Name)) updated_options.Add(opt);
                }
            }

            _serializedConfig.AllOptions = updated_options;
            _serializedConfig.Initialize();

            _serializedConfig.ValueChanged += Serialized_config_ValueChanged;

            TriggerValuesChanged();

            _intialized = true;

            Log?.Log($"Initialized configuration {Name}");
        }

        public void TriggerValuesChanged()
        {
            if (_serializedConfig is not null)
            {
                foreach (var opt in _serializedConfig.AllOptions)
                {
                    opt.OnValueChanged(new Option.ValueChangedEventArgs()
                    {
                        Name = opt.Name,
                        Value = opt.Value,
                        Save = false
                    });
                }
                    
            }
        }

        private void Serialized_config_ValueChanged(object? sender, Option.ValueChangedEventArgs e)
        {
            Log?.Log($"{Name} - {e.Name} - {e.Value}");

            if (AutoSave)
            {
                Save();
            }

            OptionValueChagned(e);
        }

        public void Add(Option option) => _serializedConfig?.Add(option);

        public void Remove(Option option) => _serializedConfig?.Remove(option);

        public void OptionValueChagned(Option.ValueChangedEventArgs e) { OptionChanged?.Invoke(this, e); }
        public event EventHandler? OptionChanged;

    }
}

