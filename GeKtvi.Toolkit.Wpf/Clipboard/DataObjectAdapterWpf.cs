using GeKtvi.Toolkit.Clipboard;
using System.Windows;

namespace GeKtvi.Toolkit.Wpf.Clipboard
{
    internal class DataObjectAdapterWpf : IDataObjectAdapter
    {
        public IDataObject DataObject => _dataObject;

        private IDataObject _dataObject;
        public DataObjectAdapterWpf(IDataObject dataObject) => _dataObject = dataObject;

        public object GetUnicodeText() => _dataObject.GetData(DataFormats.UnicodeText);

        public bool? HasCvsData() => _dataObject.GetData(DataFormats.CommaSeparatedValue) is not null;

        public bool? HasUnicodeData() => _dataObject.GetData(DataFormats.UnicodeText) is not null;

        public void SetRtfData(string sb)
        {
            _dataObject.SetData(DataFormats.Rtf, sb);
        }

        public void SetTextData(string sb)
        {
            _dataObject.SetData(DataFormats.Text, sb);
        }
    }
}
