using System;
using System.ComponentModel;
using System.Reactive.Linq;

namespace GeKtvi.Toolkit.Wpf.Reactive.NotifyPropertyChanged
{
    public static class NotifyPropertyChangedExtensions
    {
        /// <summary>
        /// Notifies when any property on the object has changed.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="propertiesToMonitor">specify properties to Monitor, or omit to monitor all property changes.</param>
        /// <returns>A observable which includes notifying on any property.</returns>
        public static IObservable<TObject?> WhenAnyPropertyChangedLight<TObject>(this TObject source, TimeSpan? throttle = null)
            where TObject : INotifyPropertyChanged
        {
            return source is null
                ? throw new ArgumentNullException(nameof(source))
                : throttle is null
                ? (IObservable<TObject?>)new ObservableNotifyPropertyChanged<TObject?>(source)
                : new ObservableNotifyPropertyChanged<TObject?>(source).Throttle(throttle.Value);
        }
    }
}
