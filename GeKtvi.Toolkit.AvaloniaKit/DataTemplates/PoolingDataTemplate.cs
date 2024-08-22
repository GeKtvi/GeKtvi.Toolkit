using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Metadata;
using Avalonia.Threading;
using Avalonia.VisualTree;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace GeKtvi.Toolkit.AvaloniaKit.DataTemplates
{
    public class PoolingDataTemplate : ITypedDataTemplate
    {
        private TimeSpan ThrottleBeforeClear { get => _throttleBeforeClear.Value; set => _throttleBeforeClear.OnNext(value); }
        private TimeSpan ThrottleBeforeFlushHot { get => _throttleBeforeFlushHot.Value; set => _throttleBeforeFlushHot.OnNext(value); }

        private BehaviorSubject<TimeSpan> _throttleBeforeClear = new(TimeSpan.FromMilliseconds(1000));
        private BehaviorSubject<TimeSpan> _throttleBeforeFlushHot = new(TimeSpan.FromMilliseconds(50));

        private int HotLimit { get; set; } = 100;
        private readonly ObjectPool<ControlContainer> _pool;

        // When items brings to view by ScrollIntoView control releases displayed items then build them again
        // This collection holds them little bit and if build requests item with same data context returns hot item
        // It allows to avoid performance cost for set data context
        private HashSet<ControlContainer> _hot = new();
        private Subject<Unit> _flushHot = new();

        [DataType]
        public Type? DataType { get; set; }
        [Content]
        [TemplateContent]
        public object? Content { get; set; }

        public PoolingDataTemplate()
        {
            var flush = _flushHot.Throttle(_ => _throttleBeforeClear);

            flush.Subscribe(ReturnAllHot);

            _pool = new(ThrottleBeforeClear,
                        () => new ControlContainer(TemplateContent.Load(Content)?.Result ?? new Control()),
                        x => flush.Select(_ => x),
                        isObjectFreeSelector: x => x.Control.IsAttachedToVisualTree() == false && x.IsDataContextRemoved);
        }

        Control? ITemplate<object?, Control?>.Build(object? parameter)
        {
            lock (_hot)
            {
                if (parameter is not null && _hot.FirstOrDefault(x => ReferenceEquals(x.Control.DataContext, parameter)) is { } hot)
                {
                    _hot.Remove(hot);
                    return hot.Control;
                }
                else
                {
                    Control item = _pool.GetObject().Control;
                    item.DetachedFromVisualTree += Flush;
                    item.DataContext = parameter;
                    return item;
                }
            }
        }

        public bool Match(object? data) => data is not null;

        private void ReturnAllHot(Unit _)
        {
            lock (_hot)
            {
                foreach (ControlContainer item in _hot)
                    Return(item);

                _hot.Clear();
            }
        }

        private void Return(ControlContainer sender)
        {
            sender.Control.DetachedFromVisualTree -= Flush;
            Dispatcher.UIThread.Invoke(() =>
            {
                sender.Control.DataContext = null;
                sender.IsDataContextRemoved = true;
            });
        }

        private void Flush(object? sender, VisualTreeAttachmentEventArgs e)
        {
            if (_hot.Count > HotLimit)
                ReturnAllHot(Unit.Default);

            lock (_hot)
                _hot.Add(new ControlContainer((sender as Control)!));
            _flushHot.OnNext(Unit.Default);
        }

        private class ControlContainer(Control control)
        {
            public Control Control { get; } = control;
            public bool IsDataContextRemoved { get; set; } = control.DataContext is null;

            public override bool Equals(object? other) => Control.Equals(other);

            public override int GetHashCode() => Control.GetHashCode();
        }
    }
}
