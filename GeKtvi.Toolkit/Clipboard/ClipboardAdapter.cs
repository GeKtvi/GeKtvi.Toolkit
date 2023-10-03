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
        public DataObjectAdapter GetDataObject()
        {
            throw new NotImplementedException();
        }

        public void SetDataObject(IDataObjectAdapter dataObj)
        {
            throw new NotImplementedException();
        }

        public object GetText()
        {
            throw new NotImplementedException();
        }
    }
}
