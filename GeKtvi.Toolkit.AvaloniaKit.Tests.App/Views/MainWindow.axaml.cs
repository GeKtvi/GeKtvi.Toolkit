using GeKtvi.Toolkit.AvaloniaKit.Clipboard;
using GeKtvi.Toolkit.AvaloniaKit.Window;
using System.Collections.Generic;

namespace GeKtvi.Toolkit.AvaloniaKit.Tests.App.Views;

public partial class MainWindow : Avalonia.Controls.Window
{
    public List<List<string>> TestData = new List<List<string>>(3)
        {
            new List<string>(4) {"1",  "2", "3", "4"},
            new List<string>(4) {"5",  "6", "7", "8"},
            new List<string>(4) {"9",  "10", "11", "12"}
        };

    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        //var clipboard = new ClipboardHelperAvalonia(this);

        //clipboard.SetClipboardData(TestData);

        //var data = clipboard.ParseClipboardData();

        var manager = new SettingsManager<WindowSettingsAvalonia>("AvaloniaKit.Tests.App", () => new WindowSettingsAvalonia());
        manager.Load().SubscribeWindow(this);
        Closed += (s, e) => manager.Save();
    }
}
