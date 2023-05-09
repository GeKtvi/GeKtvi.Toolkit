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

        public DefaultWindowSettings()
        {
            _top = 0;
            _left = 0;
            _width = 500;
            _height = 500;
            _scale = 1;
        }


        public event PropertyChangedEventHandler PropertyChanged;


        public virtual void ArrangeWindow(Window w)
        {
            w.Left = Left;
            w.Top = Top;
            w.Width = Width;
            w.Height = Height;
        }

        public virtual void ReadArrangeFromWindow(Window w)
        {
            Left = w.Left;
            Top = w.Top;
            Width = w.Width;
            Height = w.Height;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
