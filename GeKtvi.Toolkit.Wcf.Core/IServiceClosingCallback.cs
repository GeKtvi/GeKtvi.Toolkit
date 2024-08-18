using System;
using System.Reactive;

namespace NXOpenCommon.Wcf
{
    public interface IServiceClosingCallback
    {
        IObservable<Unit> Closing { get; }
        void ServiceClosing();
    }
}
