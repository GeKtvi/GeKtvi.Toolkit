using Avalonia.Input;
using Avalonia.Input.Platform;
using GeKtvi.Toolkit.Clipboard;

namespace GeKtvi.Toolkit.AvaloniaKit.Clipboard
{
    internal class DataObjectAdapterAvalonia : IDataObjectAdapter
    {
        public IClipboard Clipboard { get; }
        public DataObject DataObject { get; init; } = new();

        public DataObjectAdapterAvalonia(IClipboard clipboard) => Clipboard = clipboard;

        public object GetUnicodeText() => Clipboard.GetTextAsync().Result ?? "";

        public bool? HasCvsData() => Clipboard.GetFormatsAsync().Result.Any(x => x == "CSV");

        public bool? HasUnicodeData() => Clipboard.GetFormatsAsync().Result.Any(x => x == "Text");

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
