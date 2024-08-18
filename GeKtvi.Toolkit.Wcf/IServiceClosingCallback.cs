using System;
using System.Reactive;

namespace GeKtvi.Toolkit.Wcf.Service
{
    public interface IServiceClosingCallback
    {
        public IObservable<Unit> Closing { get; }
        public void ServiceClosing();
    }
}
