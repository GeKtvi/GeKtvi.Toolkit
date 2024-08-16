using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Diagnostics;
using System.Threading;

namespace GeKtvi.Toolkit.AvaloniaKit.Tests.App.Views;

public partial class LongTimeCreatingControl : UserControl
{
    public LongTimeCreatingControl()
    {
        InitializeComponent();
        Thread.Sleep(10);
        Debug.WriteLine($"{nameof(LongTimeCreatingControl)} was created");
    }
}