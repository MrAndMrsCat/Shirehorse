namespace Shirehorse.Core.IO
{
    public class CommandLineArgument
    {
        CommandLineArgument(string key, string[] values)
        {
            Key = key;
            Values = values;
        }

        public string Key { get; private set; }
        public string[] Values { get; private set; }

        public static List<CommandLineArgument> Parse(string[] arguments)
        {
            List<CommandLineArgument> result = new();

            return result;
        }
    }
}
