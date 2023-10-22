using System;

namespace GeKtvi.Toolkit.Window
{
    public class WindowStateAdapter : IWindowStateAdapter
    {
        public Func<bool>? CheckMaximized { get; set; }
        public Func<bool>? CheckMinimized { get; set; }
        public Func<bool>? CheckNormal { get; set; }

        public Func<bool>? SetMaximized { get; set; }
        public Func<bool>? SetMinimized { get; set; }
        public Func<bool>? SetNormal { get; set; }

        public bool IsMaximized { get => CheckMaximized?.Invoke() ?? false; }
        public bool IsMinimized { get => CheckMinimized?.Invoke() ?? false; }
        public bool IsNormal { get => CheckNormal?.Invoke() ?? false; }

        public void SetMaximizedState() => SetMaximized?.Invoke();
        public void SetMinimizedState() => SetMinimized?.Invoke();
        public void SetNormalState() => SetNormal?.Invoke();
    }
}
