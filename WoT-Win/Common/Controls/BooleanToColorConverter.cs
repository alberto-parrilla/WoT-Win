using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WoT_Win.Common.Controls
{ 
    public class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var defaultColor = new SolidColorBrush(Colors.Transparent);
            if (parameter == null) return defaultColor;
            var parameterString = parameter.ToString();
            var colorParameter = (Color)ColorConverter.ConvertFromString(parameterString);
            var brush = new SolidColorBrush(colorParameter);
            if (value == null) return defaultColor;
            var valueType = value.GetType();
            if (valueType != typeof(bool)) return defaultColor;

            return (bool) value ? brush : defaultColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }    
    }
}
