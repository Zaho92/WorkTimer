using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WorkTimer.Converter
{
    [ValueConversion(typeof(WindowState), typeof(Visibility))]
    public class WindowStateMaximizedToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WindowState state = (WindowState)value;
            switch (state)
            {
                case WindowState.Maximized:
                    return Visibility.Visible;
                default:
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack von \"WindowStateMaximizedToVisibilityConverter\" ist nicht erlaubt");
        }
    }

    [ValueConversion(typeof(WindowState), typeof(Visibility))]
    public class WindowStateNormalToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WindowState state = (WindowState)value;
            switch (state)
            {
                case WindowState.Normal:
                    return Visibility.Visible;
                default:
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(
                "ConvertBack von \"WindowStateNormalToVisibilityConverter\" ist nicht erlaubt");
        }
    }
}
