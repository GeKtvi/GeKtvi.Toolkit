using Avalonia.Input;
using Avalonia.Input.Platform;
using GeKtvi.Toolkit.Clipboard;
using System.Text;

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

        public void SetRtfData(string sb) => DataObject.Set("Rich Text Format", RtfEncode(sb));

        public void SetTextData(string sb) => DataObject.Set(DataFormats.Text, sb);

        // Simple solution to RTF encoding problem that I found: https://ru.stackoverflow.com/a/932127
        private static byte[] RtfEncode(string input)
        {
            StringBuilder sb = new StringBuilder(input.Length * 4);

            foreach (var c in input)
            {
                if (c > 127)
                {
                    string escape = "\\u" + ((Int16)c).ToString() + "?";
                    sb.Append(escape);
                }
                else
                {
                    sb.Append(c);
                }
            }

            return Encoding.ASCII.GetBytes(sb.ToString());
        }
    }
}
