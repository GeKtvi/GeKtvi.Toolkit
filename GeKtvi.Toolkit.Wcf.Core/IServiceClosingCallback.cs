using System;
using System.Reactive;

namespace GeKtvi.Toolkit.Wcf.Core
{
    public interface IServiceClosingCallback
    {
        IObservable<Unit> Closing { get; }
        void ServiceClosing();
    }
}
