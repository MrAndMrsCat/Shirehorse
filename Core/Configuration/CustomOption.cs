namespace Shirehorse.Core.Configuration
{
    /// <summary>Class <c>Config</c> provides persistant configuration with xml under the user's appdata folder, with automatic backup and UI components </summary>

    public abstract class CustomOption
    {
        public static Dictionary<string, CustomOption> Instances;
        static CustomOption()
        {
            Instances = new Dictionary<string, CustomOption>();

            var subclasses =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.IsSubclassOf(typeof(CustomOption))
                select type;

            foreach (Type type in subclasses)
            {
                var instance = (CustomOption)Activator.CreateInstance(type);
                Instances[instance.Type] = instance;
            }
        }

        public abstract string Type { get; }
        public abstract object Deserialize(string data);
        public abstract string Serialize(object obj);
        public abstract IOptionUserControl? Control { get; } // if custom usercontrol used, return new instance
    }




}

