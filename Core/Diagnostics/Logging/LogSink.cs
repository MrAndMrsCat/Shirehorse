using static Shirehorse.Core.Diagnostics.Logging.ILogging;

namespace Shirehorse.Core.Diagnostics.Logging
{
    public abstract class LogSink
    {
        public Category Level { get; set; } = Category.Information;
        public IEnumerable<LogSource> Sources => _logSources;

        private readonly List<LogSource> _logSources = new();

        public void Add(LogSource source)
        {
            _logSources.Add(source);
            source.NewLogMessage += RecieveNewLogMessage;
        }

        public void Remove(LogSource source)
        {
            source.NewLogMessage -= RecieveNewLogMessage;
            _logSources.Remove(source);
        }

        public void RecieveNewLogMessage(object? sender, LogMessage e)
        {
            if (e.Category >= Level) HandleNewLogMessage(sender, e);
        }

        protected abstract void HandleNewLogMessage(object? sender, LogMessage e);
    }
}
