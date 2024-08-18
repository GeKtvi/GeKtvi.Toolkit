using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace GeKtvi.Toolkit.Wcf.Service
{
    public class ServiceClosingCallback : IServiceClosingCallback
    {
        public IObservable<Unit> Closing => _closing.AsObservable();
        private Subject<Unit> _closing = new();

        public void ServiceClosing() => _closing.OnNext(Unit.Default);
    }
}
