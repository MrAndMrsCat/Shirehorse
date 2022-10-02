using System.Runtime.CompilerServices;
using Shirehorse.Core.Diagnostics.Logging;

namespace Shirehorse.Core.Diagnostics
{
    public class MonitoredLock
    {
        public MonitoredLock(string name = "Monitored")
        {
            Name = name + "Lock";
            LogSource = SystemLog.SystemLogSource;
        }
        public string Name { get; set; }
        public object Semaphore { get; set; } = new();
        public LogSource? LogSource { get; set; }
        public bool LogEntryAndExitEnabled { get; set; } = false;

        private System.Timers.Timer? _timeout;

        public void Invoke(int timeout, Action action, [CallerMemberName] string caller = "?")
        {
            if (timeout <= 0) throw new ArgumentException("Timeout must be > 0");

            if (_timeout is null)
            {
                _timeout = new();
                _timeout.Elapsed += (s, e) =>
                {
                    throw new TimeoutException($"Locked section exceeded timeout {timeout}ms with caller '{caller}'");
                };
            }

            _timeout.Interval = timeout;
            _timeout.Start();

            Invoke(action, caller);
        }

        private int _counter;
        private readonly object _counterSemaphore = new();
        
        public void Invoke(Action action, [CallerMemberName] string caller = "?")
        {
            lock (_counterSemaphore)
            {
                if (_counter > 0 && LogEntryAndExitEnabled)
                {
                    LogSource?.Log($"Lock WAIT - Queue:{_counter} Name:{Name} Caller:{caller}");
                }

                _counter++;
            }

            lock (Semaphore)
            {
                if (LogEntryAndExitEnabled)
                {
                    LogSource?.Log($"Lock ENTER - Name:{Name} Caller:{caller}");
                }
                action();
            }

            _timeout?.Stop();

            lock (_counterSemaphore)
            {
                _counter--;
            }

            if (LogEntryAndExitEnabled)
            {
                LogSource?.Log($"Lock EXIT - Name:{Name} Caller:{caller}");
            }
        }
    }
}
