using System.Text;
using static Shirehorse.Core.Diagnostics.Logging.ILogging;

namespace Shirehorse.Core.Diagnostics.Logging
{
    public class ConsoleDebugLog : LogSink
    {
        private static (ConsoleColor Forecolor, string Abbrivation)[] OutputFormat { get; } = new (ConsoleColor Forecolor, string Abbrivation)[]
        {
            /*Debug,       */(ConsoleColor.White,    "DBG"),
            /*StateMachine,*/(ConsoleColor.Cyan,     "FSM"),
            /*Information, */(ConsoleColor.Blue,     "INF"),
            /*UserInput,   */(ConsoleColor.Green,    "USR"),
            /*Warning,     */(ConsoleColor.Yellow,   "WRN"),
            /*Error,       */(ConsoleColor.Red,      "ERR"),
            /*Exception,   */(ConsoleColor.White,    "EX!"), // and red backcolor
        };
        
        public Fields Fields { get; set; } = Fields.All;
        public int SourceColumnWidth { get; set; } = 20;
        public bool Colored { get; set; } = true;
        public string TimestampFormat { get; set; } = "HH:mm:ss:fff";
        public bool ShortCategoryFormat { get; set; } = true;
        public bool TimeStampAllLines { get; set; } = true;

        protected override void HandleNewLogMessage(object? sender, LogMessage e)
        {
            lock (_linesBuilderLock)
            {
                if (TimeStampAllLines)
                {
                    string prefix = LinePrefix(e, Fields);

                    _linesBuilder.Clear();

                    foreach (string line in e.Message.Split('\n'))
                    {
                        _linesBuilder.Append(prefix);
                        _linesBuilder.AppendLine(line);
                    }

                    WriteLine(e.Category, _linesBuilder.ToString().Trim('\n'));
                }
                else
                {
                    WriteLine(e.Category, $"{LinePrefix(e, Fields)}{e.Message}");
                }
            }
        }

        private readonly StringBuilder _linesBuilder = new();
        private readonly object _linesBuilderLock = new();

        private string LinePrefix(LogMessage message, Fields fields)
        {
            lock (_linePrefixBuilderLock)
            {
                _linePrefixBuilder.Clear();

                _linePrefixBuilder.Append('[');

                if (fields.HasFlag(Fields.Timestamp)) _linePrefixBuilder.Append($"{message.Timestamp.ToString(TimestampFormat)}");

                if (fields.HasFlag(Fields.Category))
                {
                    if (_linePrefixBuilder.Length != 1)
                    {
                        _linePrefixBuilder.Append(' ');
                    }
                    
                    if (ShortCategoryFormat)
                    {
                        _linePrefixBuilder.Append($"{OutputFormat[(int)message.Category].Abbrivation}");
                    }
                    else
                    {
                        _linePrefixBuilder.Append($"{message.Category,-12}");
                    }
                    
                }

                if (_linePrefixBuilder.Length == 1)
                {
                    _linePrefixBuilder.Clear();
                }
                else
                {
                    _linePrefixBuilder.Append("] ");
                }

                if (fields.HasFlag(Fields.Source)) _linePrefixBuilder.Append($"{message.SourceName.PadRight(SourceColumnWidth)} | ");

                return _linePrefixBuilder.ToString();
            }
        }

        private readonly StringBuilder _linePrefixBuilder = new();
        private readonly object _linePrefixBuilderLock = new();

        private void WriteLine(Category category, string line)
        {
            lock (_writeLineLock)
            {
                if (Colored)
                { 

                    Console.ForegroundColor = OutputFormat[(int)category].Forecolor;

                    Console.BackgroundColor = category == Category.Exception
                        ? ConsoleColor.DarkRed
                        : ConsoleColor.Black;
                }
                Console.WriteLine(line);
                Console.ResetColor();
            }
        }

        private readonly object _writeLineLock = new();
    }
}
