using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace GeKtviWpfToolkit.ValueConverters
{
    internal class BooleanToVisibilityConverterReverse : IValueConverter
    {
        private BooleanToVisibilityConverter _baseConverter = new BooleanToVisibilityConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _baseConverter.Convert(!(bool)value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)_baseConverter.ConvertBack(value, targetType, parameter, culture);
        }
    }
}
