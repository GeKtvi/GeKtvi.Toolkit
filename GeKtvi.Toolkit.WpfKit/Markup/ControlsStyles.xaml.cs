using System;
using System.Windows;

namespace GeKtvi.Toolkit.WpfKit.Markup
{
    public class ControlsStyles : ResourceDictionary
    {
        public ControlsStyles() =>
            Source = new Uri($"pack://application:,,,/GeKtvi.Toolkit.WpfKit;component/Markup/ControlsStyles.xaml", UriKind.Absolute);
    }
}
