using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace GeKtvi.Toolkit.Clipboard
{
    public class ClipboardAdapter : IClipboardAdapter
    {
        public Func<IDataObjectAdapter>? GetDataObjectFunc {  get; init; }
        public Action<IDataObjectAdapter>? SetDataObjectAction { get; init; }
        public Func<object?>? GetTextAction { get; init; }

        public IDataObjectAdapter? GetDataObject() => GetDataObjectFunc?.Invoke();

        public void SetDataObject(IDataObjectAdapter dataObj) => SetDataObjectAction?.Invoke(dataObj);

        public object? GetText() => GetTextAction?.Invoke();
    }
}
