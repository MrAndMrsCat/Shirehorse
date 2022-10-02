using Shirehorse.Core.Diagnostics.Logging;

namespace Shirehorse.Core.Diagnostics
{
    public static class SystemHandler
    {
        public static IExceptionHandler? Handler { get; set; }

        public static bool SilentOnNullHandler { get; set; } = true;

        public static void Handle(Exception exception)
        {
            SystemLog.Log(exception);

            if (Handler is null)
            {
                if (!SilentOnNullHandler) throw exception;
            }
            else
            {
                Handler.Handle(exception);
            }
        }
    }
}
