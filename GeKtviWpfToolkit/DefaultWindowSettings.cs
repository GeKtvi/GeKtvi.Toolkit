using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeKtviWpfToolkit
{
    public class DefaultWindowSettings : INotifyPropertyChanged
    {
        private double _top;
        public double Top
        {
            get => _top;
            set
            {
                if (WindowState == WindowState.Minimized)
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
                if (WindowState == WindowState.Minimized)
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
                if (WindowState == WindowState.Minimized)
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
                if (WindowState == WindowState.Minimized)
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
                if (WindowState == WindowState.Minimized)
                    return;
                _scale = value;
                OnPropertyChanged(nameof(Scale));
            }
        }

        private WindowState _windowState;
        public WindowState WindowState
        {
            get => _windowState;
            set
            {
                _windowState = value;
                OnPropertyChanged(nameof(_windowState));
            }
        }

        public DefaultWindowSettings()
        {
            Top = 0;
            Left = 0;
            Width = 500;
            Height = 500;
            Scale = 1;
            WindowState = WindowState.Normal;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
