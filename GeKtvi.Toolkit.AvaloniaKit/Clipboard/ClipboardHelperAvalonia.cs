using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using GeKtvi.Toolkit.Clipboard;
using System;
using System.Collections.Generic;

namespace GeKtvi.Toolkit.WpfKit.Clipboard
{
    public class ClipboardHelperAvalonia : ClipboardHelper
    {
        private WindowBase _window;
        public ClipboardHelperAvalonia(WindowBase window)
            : this(InitializeClipboardAdapter(window),  
                   () =>  new DataObjectAdapterAvalonia(window.Clipboard ?? throw new NullReferenceException("WindowBase.Clipboard is null")))
        {
            _window = window;
        }

        private ClipboardHelperAvalonia(IClipboardAdapter clipboard, Func<IDataObjectAdapter> dataObjectAdapterFactory)
            : base(clipboard, dataObjectAdapterFactory)
        { }

        public List<string[]> ParseClipboardData(IDataObject dataObject) =>
            ParseClipboardData(new DataObjectAdapterAvalonia(_window.Clipboard ?? throw new NullReferenceException("WindowBase.Clipboard is null")));

        private static IClipboardAdapter InitializeClipboardAdapter(WindowBase window) =>
            new ClipboardAdapter()
            {
                GetDataObjectFunc = () => new DataObjectAdapterAvalonia(window.Clipboard ?? throw new NullReferenceException("WindowBase.Clipboard is null")),
                GetTextAction = () => window.Clipboard.GetTextAsync().Result,
                //SetDataObjectAction = (IDataAdapter) => System.Windows.Clipboard.SetDataObject((IDataAdapter as DataObjectAdapterAvalonia).DataObject)
            };
    }
}
