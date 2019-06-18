using System;
using System.Globalization;

using Xamarin.Forms;

namespace iotc_ble_xamarin.ValueConverters
{
    public class SelectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == parameter)
                return Color.Accent;
            return Color.Default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.Accent;
        }
    }
}