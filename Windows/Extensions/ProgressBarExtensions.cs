using Shirehorse.Core.DLL;

namespace Shirehorse.Core.Extensions
{
    public static class ProgressBarExtensioins
    {
        public static void SetColor(this ProgressBar pBar, BarColor state)
        {
            _ = User32.SendMessage((int)pBar.Handle, 1040, (int)state, 0);
        }

        public enum BarColor
        {
            Green = 1,
            Red = 2,
            Yellow = 3,
        }
    }
}
