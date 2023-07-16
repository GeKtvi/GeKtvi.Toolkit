using System;
using System.Windows;

namespace GeKtviWpfToolkit
{
    public class ControlsStyles : ResourceDictionary
    {
        public ControlsStyles()
            => Source = new Uri($"pack://application:,,,/GeKtviWpfToolkit;component/Styles/DataGridGKStyle.xaml", UriKind.Absolute);
    }
}
