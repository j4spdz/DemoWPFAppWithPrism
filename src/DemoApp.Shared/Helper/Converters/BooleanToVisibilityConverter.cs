﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DemoApp.Shared.Helper.Converters
{
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            System.Convert.ToBoolean(value)
            ? Visibility.Visible
            : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Visibility)value == Visibility.Visible) ? true : false;
        }
    }
}
