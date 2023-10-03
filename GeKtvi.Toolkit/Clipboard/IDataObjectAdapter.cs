using System;

namespace GeKtvi.Toolkit.Clipboard
{
    public interface IDataObjectAdapter
    {
        object? GetCvsData();
        object? GetData();
        object? GetUnicodeData();
        void SetRtfData(string sb);
        void SetTextData(string sb);
    }
}