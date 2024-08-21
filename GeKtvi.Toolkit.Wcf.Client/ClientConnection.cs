using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

namespace NXOpenCommon.Wcf.Client
{
    public class ClientConnection<T>(Func<IServiceClosingCallback> callbackInstanceFactory,
                                     Func<InstanceContext, Binding, EndpointAddress, T> communicationObjectFactory,
                                     Action<T> subscribeCallbackAction,
                                     string endpointAddress,
                                     int reconnectInterval = 50,
                                     int reconnectionAttempts = 100) : IDisposable, IClientConnection<T> where T : class, ICommunicationObject
    {
        public IObservable<Unit> ConnectionClosed => _connectionClosed.AsObservable();
        public Func<IServiceClosingCallback> CallbackInstanceFactory { get; } = callbackInstanceFactory;
        public Func<InstanceContext, Binding, EndpointAddress, T> CommunicationObjectFactory { get; } = communicationObjectFactory;
        public Action<T> SubscribeCallbackAction { get; } = subscribeCallbackAction;
        public string EndpointAddress { get; } = endpointAddress;
        public T? CommunicationObjectMoq { get; set; }
        public IObservable<Exception> Errors => _errors.AsObservable();
        public IServiceClosingCallback? CallbackInstance { get; private set; }

        private Subject<Exception> _errors = new();
        private Subject<Unit> _connectionClosed = new();
        private T? _client;

        public T Connect()
        {
            NetNamedPipeBinding binding = new()
            {
                MaxReceivedMessageSize = int.MaxValue,
                ReceiveTimeout = TimeSpan.MaxValue,
                SendTimeout = TimeSpan.MaxValue
            };

            try
            {
                EndpointAddress endpoint = new(new Uri(EndpointAddress));

                CallbackInstance = CallbackInstanceFactory?.Invoke();
                CallbackInstance?.Closing.Subscribe(_ => OnClosed());

                var client = CommunicationObjectFactory.Invoke(new InstanceContext(CallbackInstance), binding, endpoint);

                client.Open();
                SubscribeCallbackAction.Invoke(client);
                _client = client;
            }
            catch (CommunicationException e)
            {
                if (CommunicationObjectMoq is not null)
                {
                    _errors.OnNext(e);
                    return CommunicationObjectMoq;
                }

                if (reconnectionAttempts <= 0)
                    throw;
                TryReconnect();
            }
            return _client!;
        }

        public void Dispose()
        {
            try
            {
                _client?.Close();
                _client = null;
                GC.SuppressFinalize(this);
            }
            catch (CommunicationException e)
            {
                _errors.OnNext(e);
            }
        }

        private void TryReconnect()
        {
            reconnectionAttempts--;
            Thread.Sleep(reconnectInterval);
            Connect();
        }

        private void OnClosed()
        {
            _connectionClosed.OnNext(Unit.Default);
            _connectionClosed.OnCompleted();
        }

        ~ClientConnection() => Dispose();
    }
}