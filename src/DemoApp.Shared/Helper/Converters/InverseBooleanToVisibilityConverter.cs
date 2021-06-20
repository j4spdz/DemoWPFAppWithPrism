using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
namespace DemoApp.Shared.Helper.Converters
{
    public sealed class InverseBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            System.Convert.ToBoolean(value)
            ? Visibility.Collapsed
            : Visibility.Visible;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Visibility)value == Visibility.Collapsed) ? true : false;
        }
    }
}
