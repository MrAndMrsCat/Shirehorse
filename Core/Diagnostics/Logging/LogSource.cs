using Shirehorse.Core.Extensions;
using static Shirehorse.Core.Diagnostics.Logging.ILogging;

namespace Shirehorse.Core.Diagnostics.Logging
{
    public class LogSource
    {
        public LogSource(string name, bool addToSystemLog = true)
        {
            Name = name;

            if (addToSystemLog) SystemLog.Add(this);
        }

        public LogSource(string name, Category defaultCategory, bool addToSystemLog = true) : this(name, addToSystemLog)
        {
            DefaultCategory = defaultCategory;
        }

        public string Name { get; private set; }
        public Category DefaultCategory { get; set; } = Category.Debug;
        public bool IncludeExceptionDetails { get; set; } = true;

        public void Log(string message)
        {
            Log(DefaultCategory, message);
        }

        public void Log(Category category, string message)
        {
            NewLogMessage?.Invoke(this, new LogMessage(category, Name, message));
        }

        public void Log(Exception exception)
        {
            if (IncludeExceptionDetails)
            {
                Log(Category.Exception, $"!!! UNHANDLED EXCEPTION {exception.GetType()} !!!\n" +
                    $" - - - - - - - - - - - - - - - - - - - - - - -\n" +
                    $"{exception.ToHierarchicalString()}" +
                    $" - - - - - - - - - - - - - - - - - - - - - - -");
            }
            else
            {
                Log(Category.Exception, $"!!! UNHANDLED EXCEPTION  {exception.Message} !!!\n" );
            }
        }



        public event EventHandler<LogMessage>? NewLogMessage;

    }
}
