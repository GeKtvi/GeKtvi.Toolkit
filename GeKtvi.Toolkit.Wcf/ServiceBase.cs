using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.ServiceModel;

namespace GeKtvi.Toolkit.Wcf.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ServiceBase(Func<IntPtr> windowPointerSelector) : IApplicationService
    {
        public bool IsServiceClosed { get; private set; } = false;
        public IObservable<Exception> Errors => _errorsSubject.AsObservable();
        public IntPtr GetWindowPointer() => _windowPointerSelector.Invoke();

        private readonly Subject<Exception> _errorsSubject = new();
        private readonly Subject<Unit> _serviceShoutDowning = new();
        private readonly Func<IntPtr> _windowPointerSelector = windowPointerSelector;
        private readonly object _closeLock = new();

        public void SubscribeServiceClosing()
        {
            IServiceEvents subscriber =
                OperationContext.Current.GetCallbackChannel<IServiceEvents>();
            _serviceShoutDowning.Subscribe(_ => subscriber.ServiceClosing());
        }

        public virtual void CloseService()
        {
            lock (_closeLock)
            {
                if (IsServiceClosed)
                    throw new InvalidOperationException("Service already closed.");
                try
                {
                    _serviceShoutDowning.OnNext(Unit.Default);
                    _serviceShoutDowning.OnCompleted();
                }
                catch (ObjectDisposedException e)
                {
                    _errorsSubject.OnNext(e);
                }
                catch (CommunicationException e)
                {
                    _errorsSubject.OnNext(e);
                }
                IsServiceClosed = true;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (IsServiceClosed)
                return;
            CloseService();
        }
    }
}
