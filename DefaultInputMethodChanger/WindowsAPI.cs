using System.Runtime.InteropServices;
using System.Text;

namespace DefaultInputMethodChanger
{
    internal static class WindowsAPI
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern int SendMessage(IntPtr hWnd, int uMsg, IntPtr WParam, IntPtr LParam);

        [DllImport("user32.dll")]
        internal static extern int GetClassName(IntPtr windowHandle, StringBuilder classNameStringBuilder, int maximumCount);

        [DllImport("user32.dll")]
        internal static extern int GetWindowText(IntPtr windowHandle, StringBuilder stringBuilder, int maximumCount);

        [DllImport("user32.dll")]
        internal static extern int GetWindowTextLength(IntPtr windowHandle);

        [DllImport("user32.dll")]
        internal static extern void keybd_event(uint vk, uint scan, uint flags, uint extraInfo);

        [DllImport("imm32.dll")]
        internal static extern IntPtr ImmGetDefaultIMEWnd(IntPtr hWnd);
    }
}
