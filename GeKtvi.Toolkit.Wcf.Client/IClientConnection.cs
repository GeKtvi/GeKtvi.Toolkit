using GeKtvi.Toolkit.Wcf.Core;
using System;
using System.Reactive;

namespace GeKtvi.Toolkit.Wcf.Client
{
    public interface IClientConnection<out T>
    {
        IObservable<Unit> ConnectionClosed { get; }
        string EndpointAddress { get; }
        IObservable<Exception> Errors { get; }
        IServiceClosingCallback? CallbackInstance { get; }

        T Connect();
        void Dispose();
    }
}