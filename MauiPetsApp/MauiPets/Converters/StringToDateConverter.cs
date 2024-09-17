using MauiPetsApp.Core.Application.Formatting;
using System.Globalization;

namespace MauiPets.Converters;

public class StringToDateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        object date = null;
        if (value is string)
        {
            date = DataFormat.DateParse(value);
        }
        else if (value is DateTime)
        {
            date = (DateTime)value;
        }

        return date;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        return null;
    }
}