using Shirehorse.Core.Diagnostics.Logging;
using Shirehorse.Core.DLL;

namespace Shirehorse.Core.Extensions
{
    public static class ConsoleLogExtenstions
    {
        public static void Show(this ConsoleDebugLog consoleLog) => Kernel32.AllocConsole();
        public static void Hide(this ConsoleDebugLog consoleLog) => Kernel32.FreeConsole();
    }
}
