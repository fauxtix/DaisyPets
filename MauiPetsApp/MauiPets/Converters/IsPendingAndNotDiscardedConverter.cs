using MauiPets.Core.Application.Enums;
using MauiPets.Core.Domain.Notifications;
using System.Globalization;

namespace MauiPets.Converters
{
    // Expects the entire Notification instance (Binding Path=".")
    // Returns true when notification.IsRead == false AND Status != NotificationStatus.Descartada
    public class IsPendingAndNotDiscardedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            if (value is Notification n)
            {
                return !n.IsRead && n.Status != NotificationStatus.Descartada;
            }

            // fallback: if someone bound a tuple or simple values, treat as not pending
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
