using System.Collections.Generic;

namespace GeKtvi.Toolkit.AvaloniaKit.Tests.App.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public List<object> List { get; set; } = new();


    public MainViewModel()
    {
        for (int i = 0; i < 1000; i++)
        {
            List.Add(new object());
        }
    }
}
