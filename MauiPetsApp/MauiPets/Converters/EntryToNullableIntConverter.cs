using System.Globalization;

namespace MauiPets.Converters
{
    public class EntryToNullableIntConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 0;
            }
            var isOk = int.TryParse(value.ToString(), out var num);
            if (!isOk)
            {
                return 0;
            }
            return (int)num;
        }
    }
}
