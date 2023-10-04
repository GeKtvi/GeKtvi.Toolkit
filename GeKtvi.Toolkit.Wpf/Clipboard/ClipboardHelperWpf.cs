using GeKtvi.Toolkit.Clipboard;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GeKtvi.Toolkit.Wpf.Clipboard
{
    public class ClipboardHelperWpf : ClipboardHelper
    {
        public ClipboardHelperWpf()
            : this(InitializeClipboardAdapter(), () => new DataObjectAdapterWpf(new DataObject()))
        { }

        private ClipboardHelperWpf(IClipboardAdapter clipboard, Func<IDataObjectAdapter> dataObjectAdapterFactory)
            : base(clipboard, dataObjectAdapterFactory)
        { }

        public List<string[]> ParseClipboardData(IDataObject dataObject) =>
            ParseClipboardData(new DataObjectAdapterWpf(dataObject));

        private static IClipboardAdapter InitializeClipboardAdapter() =>
            new ClipboardAdapter()
            {
                GetDataObjectFunc = () => new DataObjectAdapterWpf(System.Windows.Clipboard.GetDataObject()),
                GetTextAction = () => System.Windows.Clipboard.GetText(),
                SetDataObjectAction = (IDataAdapter) => System.Windows.Clipboard.SetDataObject((IDataAdapter as DataObjectAdapterWpf).DataObject)
            };
    }
}
