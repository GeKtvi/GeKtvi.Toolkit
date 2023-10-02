using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GeKtvi.Toolkit.Reactive.NotifyPropertyChanged
{
    internal class ObservableNotifyPropertyChanged<TObject> : IObservable<TObject?>, IDisposable
        where TObject : INotifyPropertyChanged
    {
        private TObject? _source;
        private List<IObserver<TObject?>> _observers = new(1);

        public ObservableNotifyPropertyChanged(TObject source)
        {
            _source = source;
            _source.PropertyChanged += RiseOnNext;
        }

        public IDisposable Subscribe(IObserver<TObject?> observer)
        {
            _observers.Add(observer);
            return Disposable.Create(() => _observers.Remove(observer));
        }

        public void Dispose()
        {
            _source.PropertyChanged -= RiseOnNext;
        }

        private void RiseOnNext(object sender, PropertyChangedEventArgs e)
        {
            _observers.ForEach(subscriber => subscriber.OnNext(_source));
        }

        ~ObservableNotifyPropertyChanged() => Dispose();
    }
}
