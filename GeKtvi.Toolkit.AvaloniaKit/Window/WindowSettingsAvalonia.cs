using Avalonia;
using System.ComponentModel;
using System.Xml.Serialization;

namespace GeKtvi.Toolkit.AvaloniaKit.Window
{
    public class WindowSettingsAvalonia : Toolkit.Window.WindowSettings
    {
        [XmlIgnore]
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

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool DefaultCreated = true;

        private Avalonia.Controls.Window? _windowField;
        private Avalonia.Controls.Window _window => _windowField ?? throw new InvalidOperationException("Window must be subscribed");

        public WindowSettingsAvalonia() { }

        public WindowSettingsAvalonia(Avalonia.Controls.Window window) =>
            SubscribeWindow(window);

        public void SubscribeWindow(Avalonia.Controls.Window window)
        {
            _windowField = window;
            if (window.IsLoaded)
                SetWindowFromSettingsIfNotDefaultCrated();
            else
                _windowField.Loaded += (s, e) => SetWindowFromSettingsIfNotDefaultCrated();

            _windowField.Closing += (s, e) => PrepareForSaving();
        }

        protected virtual void SetWindowFromSettings()
        {
            _window.Position = new PixelPoint((int)Left, (int)Top);
            _window.Height = Height;
            _window.Width = Width;
            _window.WindowState = State;
        }

        protected virtual void SetFromWindow()
        {
            Top = _window.Position.Y;
            Left = _window.Position.X;
            Height = _window.Height;
            Width = _window.Width;
            State = _window.WindowState;
        }

        private void PrepareForSaving()
        {
            var tempTop = Top;
            var tempLeft = Left;
            SetFromWindow();
            if (_window.WindowState == Avalonia.Controls.WindowState.Minimized)
            {
                State = Avalonia.Controls.WindowState.Normal;
                Top = tempTop;
                Left = tempLeft;
            }
        }

        private void SetWindowFromSettingsIfNotDefaultCrated()
        {
            if (DefaultCreated)
            {
                DefaultCreated = false;
                return;
            }
            else
            {
                SetWindowFromSettings();
                OnLoad();
                _window.WindowState = State;
            }
        }
    }
}
