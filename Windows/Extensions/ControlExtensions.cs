namespace Shirehorse.Core.Extensions
{
    public static class ControlExtensions
    {
        private delegate void ThreadSafeDelegate();
        public static void ThreadSafe(this Control control, Action method)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new ThreadSafeDelegate(method));
            }
            else
            {
                method();
            }
        }

        private delegate void ThreadSafeEventHandlerDelegate(object? sender, EventArgs e);
        public static void ThreadSafe(this Control control, Action<object?, EventArgs> method, object? sender, EventArgs e)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new ThreadSafeEventHandlerDelegate(method), new object?[] { sender, e });
            }
            else
            {
                method(sender, e);
            }

        }

        private delegate void ThreadSafeEventHandlerDelegate<TEventArgs>(object? sender, TEventArgs e);
        public static void ThreadSafe<TEventArgs>(this Control control, Action<object?, TEventArgs> method, object? sender, TEventArgs e) where TEventArgs : EventArgs
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new ThreadSafeEventHandlerDelegate<TEventArgs>(method), new object?[] { sender, e });
            }
            else
            {
                method(sender, e);
            }

        }
    }
}
