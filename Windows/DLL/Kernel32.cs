using System.Runtime.InteropServices;

namespace Shirehorse.Core.DLL
{
    public static class Kernel32
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }

        public static void ResetDisplayIdleTimer() => SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED);

        [DllImport("Kernel32")]
        internal static extern void AllocConsole();

        [DllImport("Kernel32")]
        internal static extern void FreeConsole();
    }
}
