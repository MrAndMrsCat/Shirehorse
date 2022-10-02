using static Shirehorse.Core.Diagnostics.Logging.ILogging;

namespace Shirehorse.Core.Diagnostics.Logging
{
    public class LogMessage
    {
        public LogMessage(Category category, string sourceName, string message)
        {
            Timestamp = DateTime.Now;
            Category = category;
            SourceName = sourceName;
            Message = message;
        }

        public DateTime Timestamp { get; private set; }
        public Category Category { get; private set; }
        public string SourceName { get; private set; }
        public string Message { get; private set; }
    }
}
