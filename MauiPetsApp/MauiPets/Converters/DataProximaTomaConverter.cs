using System.Globalization;

namespace MauiPets.Converters
{
    public class NextDoseDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object date = null;
            if (culture.Name.ToLower().Contains("us"))
                culture = new CultureInfo("pt-PT");

            if (value is string)
            {
                if (!(string.IsNullOrEmpty(value.ToString())))
                    date = DateTime.Parse(value as string, culture).Date;
                else date = DateTime.Now.ToShortDateString();
            }
            else if (value is DateTime)
            {
                date = (DateTime)value;
            }


            TimeSpan difference = (DateTime)date - DateTime.Now.Date;
            double days = difference.TotalDays;

            if (days < 0) // in the past, Black (standard)
            {
                return Color.FromArgb("000000");
            }
            else if (days < 15) // Orange
            {
                return Color.FromArgb("FFA500");
            }
            else if (days <= 30) // Red
            {
                return Color.FromArgb("FF0000");
            }
            else
            {
                return Color.FromArgb("4682B4"); // in the future, but not in the range to inspect (< 15, < 31) => steelblue
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
