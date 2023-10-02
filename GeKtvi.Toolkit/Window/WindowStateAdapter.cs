﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeKtvi.Toolkit.Window
{
    public class WindowStateAdapter
    {
        public Func<bool> _checkMaximized;
        public Func<bool> _checkMinimized;
        public Func<bool> _checkNormal;

        public Func<bool> _setMaximized;
        public Func<bool> _setMinimized;
        public Func<bool> _setNormal;

        public bool IsMaximized { get => _checkMaximized();}
        public bool IsMinimized { get => _checkMinimized();}
        public bool IsNormal { get => _checkNormal();}

        public void SetMaximizedState() => _setMaximized();
        public void SetMinimizedState() => _setMinimized();
        public void SetNormalState() => _setNormal();
    }
}
