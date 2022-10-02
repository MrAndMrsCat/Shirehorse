using System.Runtime.InteropServices;

namespace Shirehorse.Core
{
    public class GlobalHotkey : IDisposable
    {
        public static List<string> Actions = new List<string>(); 

        public string Action;
        public HotModifierKeys ModKey;
        public Keys Key;

        private Window window = new Window();
        private int currentId;

        private static List<GlobalHotkey> Registered_Hotkeys = new List<GlobalHotkey>();

        public GlobalHotkey()
        {
            window.KeyPressed += delegate (object sender, HotKeyPressedEventArgs args)
            {
                args.Action = Action;
                HotKeyPressed?.Invoke(this, args);
            };
        }

        public void Initialize()
        {
            RegisterHotKey(ModKey, Key);
            Registered_Hotkeys.Add(this);
        }

        public static bool Exists(HotModifierKeys modKey, Keys key)
        {
            foreach (GlobalHotkey hk in Registered_Hotkeys)
                if (hk.ModKey == modKey && hk.Key == key) return true;

            return false;
        }

        public void RegisterHotKey(HotModifierKeys Modifier, Keys Key)
        {
            // increment the counter.
            currentId = currentId + 1;

            // register the hot key.
            if (!RegisterHotKey(window.Handle, currentId, (uint)Modifier, (uint)Key))
                throw new InvalidOperationException("Couldn’t register the hot key.");
        }

        public void Dispose()
        {
            for (int i = currentId; i > 0; i--)
                UnregisterHotKey(window.Handle, i);

            Registered_Hotkeys.Remove(this);

            window.Dispose();
        }

        public override string ToString() => $"{Action} = {ModKey} + {Key}";

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// Represents the window that is used internally to get the messages.
        /// </summary>
        private class Window : NativeWindow, IDisposable
        {
            private static int WM_HOTKEY = 0x0312;

            public Window()
            {
                CreateHandle(new CreateParams());
            }

            /// <summary>
            /// Overridden to get the notifications.
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                // check if we got a hot key pressed.
                if (m.Msg == WM_HOTKEY)
                {
                    // get the keys.
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                    HotModifierKeys modifier = (HotModifierKeys)((int)m.LParam & 0xFFFF);

                    // invoke the event to notify the parent.
                    OnKeyPressed(new HotKeyPressedEventArgs() { Modifier = modifier, Key = key });

                }
            }

            public void OnKeyPressed(HotKeyPressedEventArgs e) { KeyPressed?.Invoke(this, e); }
            public event EventHandler<HotKeyPressedEventArgs> KeyPressed;

            public void Dispose() => DestroyHandle();

        }

        public class HotKeyPressedEventArgs : EventArgs { public HotModifierKeys Modifier; public Keys Key; public string Action; }
        public void OnHotKeyPressed(HotKeyPressedEventArgs e) { HotKeyPressed?.Invoke(this, e); }
        public event EventHandler<HotKeyPressedEventArgs> HotKeyPressed;
    }

   
    [Flags]
    public enum HotModifierKeys : uint
    {
        Alt = 1,
        Control = 2,
        Shift = 4,
        Win = 8
    }




}
