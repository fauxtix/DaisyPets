using System.Globalization;

namespace MauiPets.Converters
{
    public class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameters = (parameter as string)?.Split('|');
            if (parameters == null || parameters.Length != 2) return value;
            return (value is bool b && b) ? parameters[0] : parameters[1];
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
