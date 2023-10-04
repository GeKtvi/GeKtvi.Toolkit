using System;

namespace GeKtvi.Toolkit.Clipboard
{
    public interface IDataObjectAdapter
    {
        bool? HasCvsData();
        object? GetUnicodeText();
        bool? HasUnicodeData();
        void SetRtfData(string sb);
        void SetTextData(string sb);
    }
}