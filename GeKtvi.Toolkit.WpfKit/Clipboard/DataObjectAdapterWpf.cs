using GeKtvi.Toolkit.Clipboard;
using System.Windows;

namespace GeKtvi.Toolkit.WpfKit.Clipboard
{
    internal class DataObjectAdapterWpf : IDataObjectAdapter
    {
        public IDataObject DataObject { get; }

        public DataObjectAdapterWpf(IDataObject dataObject) => DataObject = dataObject;

        public object GetUnicodeText() => DataObject.GetData(DataFormats.UnicodeText);

        public bool? HasCvsData() => DataObject.GetData(DataFormats.CommaSeparatedValue) is not null;

        public bool? HasUnicodeData() => DataObject.GetData(DataFormats.UnicodeText) is not null;

        public void SetRtfData(string sb)
        {
            DataObject.SetData(DataFormats.Rtf, sb);
        }

        public void SetTextData(string sb)
        {
            DataObject.SetData(DataFormats.Text, sb);
        }
    }
}
