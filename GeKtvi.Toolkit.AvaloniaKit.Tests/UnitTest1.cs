using Avalonia;
using GeKtvi.Toolkit.WpfKit.Clipboard;

namespace GeKtvi.Toolkit.AvaloniaKit.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {

        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<Application>()
                .UsePlatformDetect()
                .LogToTrace();

        [TestMethod]
        public void SetClipboardDataAndParseClipboardData_TestData_CorrectSetData()
        {
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(new[] { "" });

            var clp = new ClipboardHelperAvalonia(new Avalonia.Controls.Window());
            var dta = clp.ParseClipboardData();
        }
    }
}