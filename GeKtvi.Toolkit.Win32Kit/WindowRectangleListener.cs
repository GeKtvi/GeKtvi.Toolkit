using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace GeKtvi.Toolkit.Win32Kit
{
    public class WindowRectangleListener : IDisposable
    {
        public event EventHandler<Rectangle> RectangleChanged;
        private readonly IntPtr _targetWindow;
        private readonly WindowThreadListener _threadListener;
        private readonly bool _isDisposed = false;

        public WindowRectangleListener(IntPtr targetWindow)
        {
            _targetWindow = targetWindow;
            _threadListener = new WindowThreadListener(_targetWindow);
            _threadListener.AnyEventInvoked += OnWindowChanges;
        }

        public Rectangle GetRectangle()
        {
            RECT location = GetWindowLocation();
            return new Rectangle(
                location.Left,
                location.Top,
                location.Right - location.Left,
                location.Bottom - location.Top);
        }

        public void Dispose()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().FullName);
            _threadListener.Dispose();
        }

        protected virtual void OnWindowChanges(IntPtr hwnd, WinUserEventType eventType)
        {
            if (_targetWindow != hwnd)
                return;

            #region DEBUG
#if DEBUG
            var location = GetWindowLocation(hwnd);
            Debug.WriteLine($"{eventType,35} Left={location.Left,5}, Top={location.Top,5}, Right={location.Right,5}, Bottom={location.Bottom,5} \"{WindowTitleReader.ReadWindowTitle(hwnd),20}\" {hwnd,10}");
#endif
            #endregion

            if (eventType is WinUserEventType.EVENT_SYSTEM_FOREGROUND or
                WinUserEventType.EVENT_OBJECT_LOCATIONCHANGE or
                WinUserEventType.EVENT_SYSTEM_MOVESIZEEND)
                OnMainWindowChanged();
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        private RECT GetWindowLocation() => GetWindowLocation(_targetWindow);

        private RECT GetWindowLocation(IntPtr intPtr)
        {
            GetWindowRect(intPtr, out RECT loc);
            return loc;
        }

        private void OnMainWindowChanged()
        {
            RectangleChanged?.Invoke(this, GetRectangle());
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left, Top, Right, Bottom;
        }

        ~WindowRectangleListener() => Dispose();
    }
}
