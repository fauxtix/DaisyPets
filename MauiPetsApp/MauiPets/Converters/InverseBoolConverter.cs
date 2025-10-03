using System.Globalization;

namespace MauiPets.Converters
{
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => !(value as bool? ?? false);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => !(value as bool? ?? false);
    }
}