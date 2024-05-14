using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using GeKtvi.Toolkit.AvaloniaKit.Tests.App.ViewModels;
using GeKtvi.Toolkit.AvaloniaKit.Tests.App.Views;
using GeKtvi.Toolkit.AvaloniaKit.Window;

namespace GeKtvi.Toolkit.AvaloniaKit.Tests.App;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };

            var manager = new SettingsManager<WindowSettingsAvalonia>(
                "AvaloniaKit.Tests.App",
                () => new WindowSettingsAvalonia(),
                afterLoad: ws => ws.SubscribeWindow(desktop.MainWindow));

            desktop.MainWindow.Closed += (s, e) => manager.Dispose();

            manager.TryLoad();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
