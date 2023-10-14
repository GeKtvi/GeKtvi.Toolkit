using Avalonia.Input.Platform;
using GeKtvi.Toolkit.Clipboard;
using System.Windows;
using Avalonia.Input;

namespace GeKtvi.Toolkit.WpfKit.Clipboard
{
    internal class DataObjectAdapterAvalonia : IDataObjectAdapter
    {
        public IClipboard DataObject => _clipboard;

        private IClipboard _clipboard;
        public DataObjectAdapterAvalonia(IClipboard clipboard) => _clipboard = clipboard;

        public object GetUnicodeText() => _clipboard.GetTextAsync().Result ?? "";

        public bool? HasCvsData() => _clipboard.GetFormatsAsync().Result.Any(x => x == "CSV");

        public bool? HasUnicodeData() => _clipboard.GetFormatsAsync().Result.Any(x => x == "CSV");

        public void SetRtfData(string sb)
        {
            var obj = new DataObject();

            obj.Set(DataFormats.Text, sb);
        }

        public void SetTextData(string sb)
        {
            var obj = new DataObject();

            obj.Set(DataFormats.Text, sb);
        }
    }
}
