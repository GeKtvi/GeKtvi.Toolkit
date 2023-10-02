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
                if (WindowState.IsMinimized)
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
                if (WindowState.IsMinimized)
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
                if (WindowState.IsMinimized)
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
                if (WindowState.IsMinimized)
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

        private WindowStateAdapter _windowState;
        public WindowStateAdapter WindowState
        {
            get => _windowState;
            set
            {
                _windowState = value;
                OnPropertyChanged(nameof(_windowState));
            }
        }

        public WindowSettings()
        {
            Top = 0;
            Left = 0;
            Width = 500;
            Height = 500;
            Scale = 1;
            WindowState.SetNormalState();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnLoad()
        {
            if (WindowState.IsMinimized)
                WindowState.SetNormalState();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
