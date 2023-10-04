namespace GeKtvi.Toolkit.Wpf.Tests
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
        public void SetClipboardData_TestData_CorrectSetData()
        {
            var clipboard = new ClipboardHelperWpf();

            clipboard.SetClipboardData(TestData);
        }
    }
}