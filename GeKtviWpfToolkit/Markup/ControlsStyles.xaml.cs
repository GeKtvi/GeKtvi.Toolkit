using System;
using System.Windows;

namespace GeKtviWpfToolkit.Markup
{
    public class ControlsStyles : ResourceDictionary
    {
        public ControlsStyles()
            => Source = new Uri($"pack://application:,,,/GeKtviWpfToolkit;component/Markup/ControlsStyles.xaml", UriKind.Absolute);
    }
}
