using Avalonia.Input.Platform;

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
