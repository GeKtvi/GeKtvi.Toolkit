using GeKtvi.Toolkit.WpfKit.Clipboard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeKtvi.Toolkit.WpfKit.Tests
{
    [TestClass]
    public class ClipboardHelperWpfTests
    {

        public List<List<string>> TestData = new List<List<string>>(3)
        {
            new List<string>(4) {"1",  "2", "3", "4"},
            new List<string>(4) {"5",  "6", "7", "8"},
            new List<string>(4) {"9",  "10", "11", "12"}
        };

        [WpfTestMethod]
        public void SetClipboardDataAndParseClipboardData_TestData_CorrectSetData()
        {
            ClipboardHelperWpf clipboard = new ClipboardHelperWpf();

            clipboard.SetClipboardData(TestData);

            foreach ((string[] First, List<string> Second) item in clipboard.ParseClipboardData().Zip(TestData))
                CollectionAssert.AreEqual(item.First, item.Second);
        }
    }
}