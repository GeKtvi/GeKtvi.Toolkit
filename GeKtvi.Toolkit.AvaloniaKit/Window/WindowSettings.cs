using Avalonia.Controls;
using GeKtvi.Toolkit.Window;

namespace GeKtvi.Toolkit.AvaloniaKit.Window
{
    public class WindowSettings : Toolkit.Window.WindowSettings
    {
        public new Avalonia.Controls.WindowState State
        {
            get
            {
                return base.State switch
                {
                    Toolkit.Window.WindowState.Normal => Avalonia.Controls.WindowState.Normal,
                    Toolkit.Window.WindowState.Minimized => Avalonia.Controls.WindowState.Minimized,
                    Toolkit.Window.WindowState.Maximized => Avalonia.Controls.WindowState.Maximized,
                    Toolkit.Window.WindowState.FullScreen => Avalonia.Controls.WindowState.FullScreen,
                    _ => Avalonia.Controls.WindowState.Normal
                };
            }
            set
            {
                base.State = value switch
                {
                    Avalonia.Controls.WindowState.Normal => Toolkit.Window.WindowState.Normal,
                    Avalonia.Controls.WindowState.Minimized => Toolkit.Window.WindowState.Minimized,
                    Avalonia.Controls.WindowState.Maximized => Toolkit.Window.WindowState.Maximized,
                    Avalonia.Controls.WindowState.FullScreen => Toolkit.Window.WindowState.FullScreen,
                    _ => Toolkit.Window.WindowState.Normal
                };
            }
        }
    }
}
