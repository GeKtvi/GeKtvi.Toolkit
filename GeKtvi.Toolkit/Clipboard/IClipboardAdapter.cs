namespace GeKtvi.Toolkit.Clipboard
{
    public interface IClipboardAdapter
    {
        IDataObjectAdapter? GetDataObject();
        object? GetText();
        void SetDataObject(IDataObjectAdapter dataObj);
    }
}