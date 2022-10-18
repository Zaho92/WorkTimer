using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;

namespace WorkTimer.Converter
{
    [ValueConversion(typeof(WindowState), typeof(Visibility))]
    public class WindowMinimizeButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Window window)
            {
                if (window.ResizeMode == ResizeMode.NoResize)
                {
                    return Visibility.Collapsed;
                }
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack von \"WindowMinimizeButtonVisibilityConverter\" ist nicht erlaubt");
        }
    }

    [ValueConversion(typeof(WindowState), typeof(Visibility))]
    public class WindowMaximizeButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Window window)
            {
                if (window.ResizeMode == ResizeMode.NoResize)
                {
                    return Visibility.Collapsed;
                }
                switch (window.WindowState)
                {
                    case WindowState.Maximized: 
                        return Visibility.Collapsed;
                }
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack von \"WindowMaximizeButtonVisibilityConverter\" ist nicht erlaubt");
        }
    }

    [ValueConversion(typeof(WindowState), typeof(Visibility))]
    public class WindowRestoreButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Window window)
            {
                if (window.ResizeMode == ResizeMode.NoResize)
                {
                    return Visibility.Collapsed;
                }
                switch (window.WindowState)
                {
                    case WindowState.Normal:
                        return Visibility.Collapsed;
                }
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(
                "ConvertBack von \"WindowRestoreButtonVisibilityConverter\" ist nicht erlaubt");
        }
    }
}