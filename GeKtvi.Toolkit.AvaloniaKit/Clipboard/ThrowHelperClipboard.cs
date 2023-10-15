using Avalonia.Input.Platform;
using GeKtvi.Toolkit.Clipboard;
using GeKtvi.Toolkit.WpfKit.Clipboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeKtvi.Toolkit.AvaloniaKit.Clipboard
{
    internal static class ThrowHelperClipboard
    {
        public static IClipboard ThrowClipboardIsNull() => 
            throw new NullReferenceException("WindowBase.Clipboard is null");

        public static DataObjectAdapterAvalonia ThrowDataObjectAdapterHasIncorrectType() =>
            throw new NullReferenceException($"DataObjectAdapter Has Incorrect Type must be {typeof(DataObjectAdapterAvalonia)}");
    }
}
