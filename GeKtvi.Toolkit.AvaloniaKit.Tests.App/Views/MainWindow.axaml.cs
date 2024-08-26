using Avalonia.Threading;
using GeKtvi.Toolkit.AvaloniaKit.Clipboard;
using GeKtvi.Toolkit.AvaloniaKit.Window;
using System.Collections.Generic;
using System.Threading;

namespace GeKtvi.Toolkit.AvaloniaKit.Tests.App.Views;

public partial class MainWindow : Avalonia.Controls.Window
{
    public List<List<string>> TestData = new(3)
    {
        new List<string>(4) {"1А",  "2Б", "3В", "4Г"},
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
        Dispatcher.UIThread.Invoke(() =>
        {
            var data = clipboard.ParseClipboardData();
        });
        Dispatcher.UIThread.Invoke(() =>
        {
            clipboard.SetClipboardData(TestData);
        });
    }
}
