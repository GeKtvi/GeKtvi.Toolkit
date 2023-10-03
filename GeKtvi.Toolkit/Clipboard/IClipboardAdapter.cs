namespace GeKtvi.Toolkit.Clipboard
{
    public interface IClipboardAdapter
    {
        DataObjectAdapter GetDataObject();
        object GetText();
        void SetDataObject(IDataObjectAdapter dataObj);
    }
}