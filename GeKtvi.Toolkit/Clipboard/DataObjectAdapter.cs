using System;
using System.Text;

namespace GeKtvi.Toolkit.Clipboard
{
    public class DataObjectAdapter : IDataObjectAdapter
    {
        public Action? SetRtfDataAction {  get; init; }
        public Action? SetSetTextDataAction { get; init; }
        public Func<bool>? HasCvsDataFunc { get; init; }
        public Func<object>? GetDataFunc { get; init; }
        public Func<bool>? HasUnicodeDataFunc { get; init; }
        public object DataObject { get; init; }

        public DataObjectAdapter(object dataObject)
        {
            DataObject = dataObject;
        }

        public void SetRtfData(string sb) => SetRtfDataAction?.Invoke();

        public void SetTextData(string sb) => SetSetTextDataAction?.Invoke();

        public bool? HasCvsData() => HasCvsDataFunc?.Invoke();

        public object? GetUnicodeText() => GetDataFunc?.Invoke();

        public bool? HasUnicodeData() => HasUnicodeDataFunc?.Invoke();
    }
}