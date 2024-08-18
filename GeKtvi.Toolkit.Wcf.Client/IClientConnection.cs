using NXOpenCommon.Wcf;
using System;
using System.Reactive;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace NXOpenCommon.Wcf.Client
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