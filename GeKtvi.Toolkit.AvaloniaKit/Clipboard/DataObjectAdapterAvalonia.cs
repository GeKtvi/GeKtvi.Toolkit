using Avalonia.Input;
using Avalonia.Input.Platform;
using GeKtvi.Toolkit.Clipboard;

namespace GeKtvi.Toolkit.WpfKit.Clipboard
{
    internal class DataObjectAdapterAvalonia : IDataObjectAdapter
    {
        public IClipboard Clipboard => _clipboard;
        public DataObject DataObject { get; init; } = new();

        private IClipboard _clipboard;
        public DataObjectAdapterAvalonia(IClipboard clipboard) => _clipboard = clipboard;

        public object GetUnicodeText() => _clipboard.GetTextAsync().Result ?? "";

        public bool? HasCvsData() => _clipboard.GetFormatsAsync().Result.Any(x => x == "CSV");

        public bool? HasUnicodeData() => _clipboard.GetFormatsAsync().Result.Any(x => x == "Text");

        public void SetRtfData(string sb)
        {
            DataObject.Set("Rich Text Format", sb);
        }

        public void SetTextData(string sb)
        {
            DataObject.Set(DataFormats.Text, sb);
        }
    }
}
