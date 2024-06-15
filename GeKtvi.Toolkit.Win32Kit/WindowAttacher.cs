using System;
using System.Runtime.InteropServices;

namespace GeKtvi.Toolkit.Win32Kit
{
    public class WindowAttacher
    {
        public bool BringToFrontOnAttach { get; set; } = true;
        private readonly IntPtr _parent;
        private readonly IntPtr _child;

        public WindowAttacher(IntPtr parent, IntPtr child)
        {
            _parent = parent;
            _child = child;
        }

        public void AttachToWindow() =>
            SetOwner(_parent, _child);

        public void DetachFromWindow() =>
            SetOwner(IntPtr.Zero, _child);

        [DllImport("user32.dll")]
        private static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowLongPtr(IntPtr h, int tok, IntPtr val);

        private void SetOwner(IntPtr parent, IntPtr child)
        {
            ReparentWindow(child, parent);
            if (BringToFrontOnAttach)
                BringWindowToTop(child);
        }

        private static IntPtr ReparentWindow(IntPtr childHandle, IntPtr newParentHandle) =>
            SetWindowLongPtr(childHandle, -8, newParentHandle);
    }
}
