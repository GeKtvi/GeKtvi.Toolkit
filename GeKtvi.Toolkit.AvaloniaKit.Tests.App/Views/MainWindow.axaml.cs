using GeKtvi.Toolkit.AvaloniaKit.Clipboard;
using GeKtvi.Toolkit.AvaloniaKit.Window;
using System.Collections.Generic;

namespace GeKtvi.Toolkit.AvaloniaKit.Tests.App.Views;

public partial class MainWindow : Avalonia.Controls.Window
{
    public List<List<string>> TestData = new(3)
    {
        new List<string>(4) {"1",  "2", "3", "4"},
        new List<string>(4) {"5",  "6", "7", "8"},
        new List<string>(4) {"9",  "10", "11", "12"}
    };

    public MainWindow()
    {
        InitializeComponent();
        var conf = AppConfigHelper.LoadArrayConfigs<string>(filePattern: "*File.config");

        var manager = new SettingsManager<WindowSettingsAvalonia>(
            "AvaloniaKit.Tests.App",
            () => new WindowSettingsAvalonia(),
            afterLoad: ws => ws.SubscribeWindow(this));

        Loaded += MainWindow_Loaded;

    }

    private void MainWindow_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var clipboard = new ClipboardHelperAvalonia(this);
        clipboard.SetClipboardData(TestData);
        var data = clipboard.ParseClipboardData();
    }
}
