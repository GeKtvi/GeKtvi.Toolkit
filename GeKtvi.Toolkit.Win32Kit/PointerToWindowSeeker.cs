using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GeKtvi.Toolkit.Win32Kit
{
    public static class PointerToWindowSeeker
    {
        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(uint dwThreadId, EnumWindowsProc lpfn, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint GetClassName(IntPtr hwnd, StringBuilder lpClassName, uint nMaxCount);

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        // Delegate to filter which windows to include 
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        /// <summary>
        /// Find IntPtr for window in current thread
        /// </summary>
        /// <param name="dialogTitle">Title of dialog used to identify dialogs window</param>
        /// <param name="windowClassName">
        /// Window Class of dialog used to identify dialogs window                                             <br />
        /// can be taken from WindowClasses or                                                                 <br />
        ///  https://learn.microsoft.com/en-us/windows/win32/winmsg/about-window-classes                       <br />
        /// </param>
        /// <returns></returns>
        public static IntPtr FindWindowPointer(string dialogTitle, string windowClassName, uint threadID)
        {
            IntPtr dialog = IntPtr.Zero;
            EnumThreadWindows(threadID, (hWnd, lParam) =>
            {
                string className = string.Empty;
                string windowTitle = WindowTitleReader.ReadWindowTitle(hWnd);
                if (windowTitle == dialogTitle)
                {
                    StringBuilder stringBuilder = new(256);
                    GetClassName(hWnd, stringBuilder, (uint)stringBuilder.Capacity);
                    className = stringBuilder.ToString();

                    if (className == windowClassName || windowClassName == null)
                    {
                        dialog = hWnd;
                        return false;
                    }
                }
                return true;
            }, IntPtr.Zero);
            return dialog;
        }

        public static IntPtr FindWindowPointer(string dialogTitle, string windowClassName) =>
            FindWindowPointer(dialogTitle, windowClassName, GetCurrentThreadId());

        public static IntPtr FindWindowPointer(string dialogTitle) =>
            FindWindowPointer(dialogTitle, null);
    }
}
