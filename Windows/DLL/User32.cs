using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Shirehorse.Core.DLL
{
    public static class User32
	{
		[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
		public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

		[Flags]
		public enum M_Win
		{
			NoMove = 0X2,
			NoSize = 1,
			NoZOrder = 0X4,
			ShowWindow = 0x0040,
		}

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);



		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		[DllImport("User32.dll")]
		public static extern bool ShowWindow(IntPtr handle, int nCmdShow);

		public enum ShowWindowCmd
		{
			SW_HIDE,
			SW_SHOWNORMAL,
			SW_SHOWMINIMIZED,
			SW_MAXIMIZE,
			SW_SHOWMAXIMIZED,
			SW_SHOWNOACTIVATE,
			SW_SHOW,
			SW_MINIMIZE,
			SW_SHOWMINNOACTIVE,
			SW_SHOWNA,
			SW_RESTORE,
			SW_SHOWDEFAULT,
			SW_FORCEMINIMIZE,
		}

		[DllImport("user32.dll")]
		public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlags flags);

		[Flags()]
		public enum RedrawWindowFlags : uint
		{
			Invalidate = 0x1,
			InternalPaint = 0x2,
			Erase = 0x4,
			Validate = 0x8,
			NoInternalPaint = 0x10,
			NoErase = 0x20,
			NoChildren = 0x40,
			AllChildren = 0x80,
			UpdateNow = 0x100,
			EraseNow = 0x200,
			Frame = 0x400,
			NoFrame = 0x800
		}


		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left;        // x position of upper-left corner
			public int Top;         // y position of upper-left corner
			public int Right;       // x position of lower-right corner
			public int Bottom;      // y position of lower-right corner
		}

		public static Rectangle GetWindowRectangle(IntPtr handle)
		{
			RECT rct;
			GetWindowRect(new HandleRef(new object(), handle), out rct);
			return new Rectangle()
			{
				X = rct.Left,
				Y = rct.Top,
				Width = rct.Right - rct.Left,
				Height = rct.Bottom - rct.Top
			};
		}

		[DllImport("User32.dll")]
		public static extern Int32 SendMessage(int hWnd, int Msg, int wParam, StringBuilder lParam);

		[DllImport("User32.dll")]
		public static extern Int32 SendMessage(int hWnd, int Msg, int wParam, int lParam);

		public const int WM_GETTEXT = 0x000D;
		public const int WM_GETTEXTLENGTH = 0x000E;
		public const int WM_CLOSE = 0x0010;

		public static string GetWindowText(IntPtr handle)
		{
			int txtLength = SendMessage(handle.ToInt32(), WM_GETTEXTLENGTH, 0, 0);
			StringBuilder sbText = new StringBuilder(txtLength + 1);
			SendMessage(handle.ToInt32(), WM_GETTEXT, sbText.Capacity, sbText);
			return sbText.ToString();
		}

		public const int WM_LBUTTONDOWN = 0x0201;
		public const int WM_LBUTTONUP = 0x0202;
		public static void SendMouseClick(int iHandle, int X, int Y)
		{
			//int X = X - WindowRect.left;
			//int Y = Y - winsowRect.top;
			int lparm = (Y << 16) + X;
			SendMessage(iHandle, WM_LBUTTONDOWN, 0, lparm);
			SendMessage(iHandle, WM_LBUTTONUP, 0, lparm);
		}

		public static void CloseWindow(IntPtr handle)
		{
			SendMessage(handle.ToInt32(), WM_CLOSE, 0, 0);
		}

		[DllImport("User32.Dll")]
		public static extern void GetClassName(int hWnd, StringBuilder s, int nMaxCount);

		public static string GetClassName(IntPtr handle)
		{
			StringBuilder sbClass = new StringBuilder(256);
			GetClassName(handle.ToInt32(), sbClass, sbClass.Capacity);
			return sbClass.ToString();
		}


		public delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

		public static IEnumerable<IntPtr> EnumerateProcessWindowHandles(int processId) =>
			EnumerateProcessWindowHandles(Process.GetProcessById(processId));

		public static IEnumerable<IntPtr> EnumerateProcessWindowHandles(Process process)
		{
			var handles = new List<IntPtr>();

			foreach (ProcessThread thread in process.Threads)
				EnumThreadWindows(thread.Id, (hWnd, lParam) => { handles.Add(hWnd); return true; }, IntPtr.Zero);

			return handles;
		}


		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWindow(IntPtr hWnd);


		[Flags]
		public enum MouseEventFlags
		{
			LeftDown = 0x00000002,
			LeftUp = 0x00000004,
			MiddleDown = 0x00000020,
			MiddleUp = 0x00000040,
			Move = 0x00000001,
			Absolute = 0x00008000,
			RightDown = 0x00000008,
			RightUp = 0x00000010
		}

		[DllImport("user32.dll", EntryPoint = "SetCursorPos")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetCursorPos(int x, int y);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetCursorPos(out MousePoint lpMousePoint);

		[DllImport("user32.dll")]
		private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

		public static void SetCursorPosition(int x, int y)
		{
			SetCursorPos(x, y);
		}

		public static void SetCursorPosition(MousePoint point)
		{
			SetCursorPos(point.X, point.Y);
		}

		public static MousePoint GetCursorPosition()
		{
			MousePoint currentMousePoint;
			var gotPoint = GetCursorPos(out currentMousePoint);
			if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
			return currentMousePoint;
		}

		public static void MouseEvent(MouseEventFlags value)
		{
			MousePoint position = GetCursorPosition();

			mouse_event
				((int)value,
				 position.X,
				 position.Y,
				 0,
				 0)
				;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MousePoint
		{
			public int X;
			public int Y;

			public MousePoint(int x, int y)
			{
				X = x;
				Y = y;
			}
		}

	}
}
