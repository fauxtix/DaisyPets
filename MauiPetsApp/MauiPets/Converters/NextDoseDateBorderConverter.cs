using System.Globalization;

namespace MauiPets.Converters
{
    public class NextDoseDateBorderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object date = null;
            if (value is string str && !string.IsNullOrEmpty(str))
                date = DateTime.Parse(str);
            else if (value is DateTime dt)
                date = dt;
            else
                date = DateTime.Now;

            TimeSpan difference = ((DateTime)date).Date - DateTime.Now.Date;
            double days = difference.TotalDays;

            if (days == 0)
                return Colors.Green; // Borda verde quando é hoje
            else
                return Colors.Transparent; // sem borda nos outros casos
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
