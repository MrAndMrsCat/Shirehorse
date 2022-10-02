using System.Text;
using static Shirehorse.Core.Diagnostics.Logging.ILogging;

namespace Shirehorse.Core.Diagnostics.Logging
{
    public class TextFileLog : LogSink
    {
        public TextFileLog()
        {
            _flushTimer.Elapsed += (s, e) => { Flush(); };
        }

        public Fields Fields { get; set; } = Fields.All;
        public int SourceColumnWidth { get; set; } = 40;
        public string? TextLogDirectoryPath { get; set; }
        public string LogFileName { get; set; } = "Event Log";
        public double FlushInterval // (ms). If 0, do not cache, always append to text file
        {
            get => _flushTimer.Interval;
            set => _flushTimer.Interval = value;
        }
        private string? FilePath => TextLogDirectoryPath is null
            ? null
            : Path.Combine(TextLogDirectoryPath, $"{DateTime.Now:yyyy-MM-dd HH}-00 {LogFileName}.txt");
        private static string LogSourceName => "Text Logger";

        private readonly System.Timers.Timer _flushTimer = new () { Enabled = true, Interval = 15000 };

        protected override void HandleNewLogMessage(object? sender, LogMessage e)
        {
            if (e.Category >= Level)
            {
                LogLine(BuildLine(e, Fields));
            }
        }

        private string BuildLine(LogMessage e, Fields fields)
        {
            lock (_stringBuilderLock)
            {
                _stringBuilder.Clear();

                if (fields.HasFlag(Fields.Timestamp)) _stringBuilder.Append($"{e.Timestamp:HH:mm:ss: fff} | ");
                if (fields.HasFlag(Fields.Category)) _stringBuilder.Append($"{e.Category,-12} | ");
                if (fields.HasFlag(Fields.Source)) _stringBuilder.Append($"{e.SourceName.PadRight(SourceColumnWidth)} | ");
                _stringBuilder.Append(e.Message);

                return _stringBuilder.ToString();
            }
        }

        private readonly StringBuilder _stringBuilder = new StringBuilder();
        private readonly object _stringBuilderLock = new ();

        private void LogLine(string message)
        {

            if (FlushInterval == 0)
            {
                if (FilePath is not null)
                {
                    try
                    {
                        File.AppendAllText(FilePath, $"{message}\n");
                        _textLogBuffer.Clear();
                    }
                    catch (Exception ex) { SystemLog.Log(ex); }
                }
            }
            else
            {
                lock (_textLogBufferLock) _textLogBuffer.AppendLine(message);
            }
        }

        public void Flush()
        {
            if (_textLogBuffer.Length > 0 && TextLogDirectoryPath is not null && FilePath is not null)
            {
                Directory.CreateDirectory(TextLogDirectoryPath);

                lock (_textLogBufferLock)
                {
                    try
                    {
                        File.AppendAllText(FilePath, _textLogBuffer.ToString());
                        _textLogBuffer.Clear();
                    }
                    catch (Exception ex) { SystemLog.Log(ex); }
                }
            }
        }



        private readonly StringBuilder _textLogBuffer = new ();
        private readonly object _textLogBufferLock = new();
    }
}
