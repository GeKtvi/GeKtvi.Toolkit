using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GeKtvi.Toolkit.Win32Kit
{
    internal class WindowTitleReader
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        public static string ReadWindowTitle(IntPtr hWnd)
        {
            int length = GetWindowTextLength(hWnd) + 1;
            StringBuilder title = new(length);
            GetWindowText(hWnd, title, length);
            return title.ToString();
        }
    }
}
