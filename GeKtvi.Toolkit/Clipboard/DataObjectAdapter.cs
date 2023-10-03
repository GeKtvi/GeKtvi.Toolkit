using System;
using System.Text;

namespace GeKtvi.Toolkit.Clipboard
{
    public class DataObjectAdapter : IDataObjectAdapter
    {
        public Action? SetRtfDataAction {  get; init; }
        public Action? SetSetTextDataAction { get; init; }
        public Func<object>? GetCvsDataFunc { get; init; }
        public Func<object>? GetDataFunc { get; init; }
        public Func<object>? GetUnicodeDataFunc { get; init; }

        public void SetRtfData(string sb) => SetRtfDataAction?.Invoke();

        public void SetTextData(string sb) => SetSetTextDataAction?.Invoke();

        public object? GetCvsData() => GetCvsDataFunc?.Invoke();

        public object? GetData() => GetDataFunc?.Invoke();

        public object? GetUnicodeData() => GetUnicodeDataFunc?.Invoke();
    }
}