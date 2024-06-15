using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace GeKtvi.Toolkit.Win32Kit
{
    public class WindowThreadListener : IDisposable
    {
        public event Action<IntPtr, WinUserEventType> AnyEventInvoked;
        private readonly uint _processId;
        private readonly uint _threadId;
        private readonly IntPtr _targetWindow;
        private readonly IntPtr _hook;
        private bool _disposed = false;

        // Needed to prevent the GC from sweeping up our callback
        private readonly WinEventDelegate _winEventDelegate;

        private delegate void WinEventDelegate(
            IntPtr hWinEventHook,
            uint eventType,
            IntPtr hwnd,
            int idObject,
            int idChild,
            uint dwEventThread,
            uint dwmsEventTime);

        public WindowThreadListener(IntPtr targetWindow)
        {
            _targetWindow = targetWindow;

            _threadId = GetWindowThreadProcessId(_targetWindow, out _processId);
            ThrowOnWin32Error("Failed to get process id");

            _winEventDelegate = HookCallback;

            _hook = SetWinEventHook(
                (uint)WinUserEventType.EVENT_MIN,
                (uint)WinUserEventType.EVENT_MAX,
                _targetWindow,
                _winEventDelegate,
                _processId,
                _threadId,
                0);
        }

        public void Dispose()
        {
            if (_disposed)
                return;
            UnhookWinEvent(_hook);
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private void HookCallback(IntPtr hWinEventHook, uint eventType, IntPtr hWnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            AnyEventInvoked?.Invoke(hWnd, (WinUserEventType)eventType);
        }

        private static void ThrowOnWin32Error(string message)
        {
            int err = Marshal.GetLastWin32Error();
            if (err != 0)
                throw new Win32Exception(err, message);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        ~WindowThreadListener() => Dispose();
    }
}
