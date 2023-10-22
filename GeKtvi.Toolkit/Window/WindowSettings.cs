using System.ComponentModel;

namespace GeKtvi.Toolkit.Window
{
    public class WindowSettings : INotifyPropertyChanged
    {
        private double _top;
        public double Top
        {
            get => _top;
            set
            {
                if (State == WindowState.Minimized)
                    return;
                _top = value;
                OnPropertyChanged(nameof(Width));
            }
        }

        private double _left;
        public double Left
        {
            get => _left;
            set
            {
                if (State == WindowState.Minimized)
                    return;
                _left = value;
                OnPropertyChanged(nameof(Width));
            }
        }

        private double _width;
        public double Width
        {
            get => _width;
            set
            {
                if (State == WindowState.Minimized)
                    return;
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }

        private double _height;
        public double Height
        {
            get => _height;
            set
            {
                if (State == WindowState.Minimized)
                    return;
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        private double _scale;
        public double Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                OnPropertyChanged(nameof(Scale));
            }
        }

        private WindowState _state = WindowState.Normal;
        public WindowState State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged(nameof(_state));
            }
        }

        public WindowSettings()
        {
            Top = 0;
            Left = 0;
            Width = 500;
            Height = 500;
            Scale = 1;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnLoad()
        {
            if (State == WindowState.Maximized || State == WindowState.FullScreen)
                State = WindowState.Normal;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
