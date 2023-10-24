using Avalonia;

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

        public void SetWindowFromSettings(Avalonia.Controls.Window window)
        {
            window.Position = new PixelPoint((int)Left, (int)Top);
            window.Height = Height;
            window.Width = Width;
        }

        public void SetFromWindow(Avalonia.Controls.Window window)
        {
            Top = window.Position.Y;
            Left = window.Position.X;
            Height = window.Height;
            Width = window.Width;
        }
    }
}
