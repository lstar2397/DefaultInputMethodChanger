using System.Text;

namespace DefaultInputMethodChanger
{
    internal static class WindowsAPIUtility
    {
        private const int WM_IME_CONTROL = 643;
        private const int IME_CMODE_ALPHANUMERIC = 0x0;
        private const int IME_CMODE_NATIVE = 0x1;
        private const int VK_HANGUL = 0x15;
        private const int KEYEVENTF_KEYDOWN = 0x00;
        private const int KEYEVENTF_KEYUP = 0x02;

        internal static string GetClassName(IntPtr handle)
        {
            var className = new StringBuilder(256);
            WindowsAPI.GetClassName(handle, className, className.Capacity);

            return className.ToString();
        }

        internal static string GetWindowText(IntPtr handle)
        {
            var length = WindowsAPI.GetWindowTextLength(handle);
            var windowText = new StringBuilder(length + 1);
            WindowsAPI.GetWindowText(handle, windowText, windowText.Capacity);

            return windowText.ToString();
        }

        internal static IMEConversionMode GetIMEConversionMode(IntPtr handle)
        {
            var imeHandle = WindowsAPI.ImmGetDefaultIMEWnd(handle);
            var status = WindowsAPI.SendMessage(imeHandle, WM_IME_CONTROL, new IntPtr(0x5), IntPtr.Zero);

            switch (status)
            {
                case IME_CMODE_ALPHANUMERIC:
                    return IMEConversionMode.ALPHANUMERIC;
                case IME_CMODE_NATIVE:
                    return IMEConversionMode.NATIVE;
                default:
                    throw new Exception("Unknown IME Conversion Mode");
            }
        }

        internal static void ToggleInputMethod(IntPtr handle)
        {
            IntPtr prevHandle = WindowsAPI.GetForegroundWindow();

            if (prevHandle != handle)
                WindowsAPI.SetForegroundWindow(handle);

            WindowsAPI.keybd_event(VK_HANGUL, 0, KEYEVENTF_KEYDOWN, 0);
            WindowsAPI.keybd_event(VK_HANGUL, 0, KEYEVENTF_KEYUP, 0);

            WindowsAPI.SetForegroundWindow(prevHandle);
        }
    }
}
