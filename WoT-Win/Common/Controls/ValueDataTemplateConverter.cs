using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WoT_Win.Common.Controls
{
    public class ValueDataTemplateConverter : IValueConverter
    {
        public DataTemplate TemplateNull { get; set; }
        public DataTemplate TemplateFilled { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return TemplateNull;
            return TemplateFilled;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
