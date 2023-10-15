using Avalonia.Controls;
using Avalonia.Input;
using GeKtvi.Toolkit.Clipboard;

namespace GeKtvi.Toolkit.AvaloniaKit.Clipboard
{
    public class ClipboardHelperAvalonia : ClipboardHelper
    {
        private WindowBase? _window;
        public ClipboardHelperAvalonia(WindowBase window)
            : this(InitializeClipboardAdapter(window),
                   () => new DataObjectAdapterAvalonia(window.Clipboard ?? ThrowHelperClipboard.ThrowClipboardIsNull()))
        {
            _window = window;
        }

        private ClipboardHelperAvalonia(IClipboardAdapter clipboard, Func<IDataObjectAdapter> dataObjectAdapterFactory)
            : base(clipboard, dataObjectAdapterFactory)
        { }

        public List<string[]> ParseClipboardData(IDataObject dataObject) =>
            ParseClipboardData(new DataObjectAdapterAvalonia(_window?.Clipboard ?? ThrowHelperClipboard.ThrowClipboardIsNull()));

        private static IClipboardAdapter InitializeClipboardAdapter(WindowBase window) =>
            new ClipboardAdapter()
            {
                GetDataObjectFunc = () => new DataObjectAdapterAvalonia(window.Clipboard ?? ThrowHelperClipboard.ThrowClipboardIsNull()),
                GetTextAction = () => window.Clipboard?.GetTextAsync().Result,
                SetDataObjectAction = (IDataAdapter) =>
                    window.Clipboard?.SetDataObjectAsync(
                        (IDataAdapter as DataObjectAdapterAvalonia ?? ThrowHelperClipboard.ThrowDataObjectAdapterHasIncorrectType()).DataObject
                    ).Wait()
            };
    }
}
