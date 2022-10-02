using static Shirehorse.Core.Diagnostics.Logging.ILogging;

namespace Shirehorse.Core.Diagnostics.Logging
{
    public static class SystemLog
    {
        static SystemLog()
        {
            SystemLogSource = new("System") 
            { 
                IncludeExceptionDetails = true,
            };
        }

        public static LogSource SystemLogSource { get; } 
        public static ConsoleDebugLog? ConsoleLog { get; set; }
        public static List<LogSink> LogSinks { get; set; } = new();


        private static readonly List<LogSource> _logSources = new();

        private static int _maxLogSourceNameLength = 20;

        public static void EnableConsoleLog()
        {
            if(ConsoleLog is null)
            {
                ConsoleLog = new() { Level = Category.Debug };
                LogSinks.Add(ConsoleLog);
            }
        }

        public static void Add(LogSource logSource)
        {
            if (!_logSources.Contains(logSource))
            {
                logSource.NewLogMessage += LogSource_NewLogMessage;
                _logSources.Add(logSource);

                _maxLogSourceNameLength = Math.Max(_maxLogSourceNameLength, logSource.Name.Length);
            }
        }

        public static void Remove(LogSource logSource)
        {
            if (!_logSources.Contains(logSource))
            {
                logSource.NewLogMessage -= LogSource_NewLogMessage;
                _logSources.Remove(logSource);
            }
        }

        private static void LogSource_NewLogMessage(object? sender, LogMessage e)
        {
            foreach (LogSink sink in LogSinks)
            {
                sink.RecieveNewLogMessage(sender, e);
            }

            if (ConsoleLog is not null) ConsoleLog.SourceColumnWidth = _maxLogSourceNameLength;
        }

        public static void Log(string message)
        {
            SystemLogSource.Log(message);
        }

        public static void Log(Category category, string message)
        {
            SystemLogSource.Log(category, message);
        }

        public static void Log(Exception exception)
        {
            SystemLogSource.Log(exception);
        }
    }
}
