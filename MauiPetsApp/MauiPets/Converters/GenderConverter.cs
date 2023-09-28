namespace MauiPets.Converters
{
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string gender)
            {
                if (gender == "F")
                    return "Female";
                else if (gender == "M")
                    return "Male";
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException(); // Optional: Implement this if you need two-way binding.
        }
    }
}
