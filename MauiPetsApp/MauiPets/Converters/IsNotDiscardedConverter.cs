using MauiPets.Core.Application.Enums;
using System.Globalization;

namespace MauiPets.Converters
{
    // Converts a Status value (int or NotificationStatus) to a bool: true when NOT Descartada
    public class IsNotDiscardedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return true;

            // If value is the enum
            if (value is NotificationStatus ns)
                return ns != NotificationStatus.Descartada;

            // If value is int (DB mapping), compare numeric value
            if (value is int i)
                return i != (int)NotificationStatus.Descartada;

            // Try parse string fallback
            if (int.TryParse(value.ToString(), out var parsed))
                return parsed != (int)NotificationStatus.Descartada;

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
