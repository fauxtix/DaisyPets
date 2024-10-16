using System.Globalization;

namespace MauiPets.Converters
{
    public class IdadeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string dateString && DateTime.TryParse(dateString, out DateTime birthDate))
            {
                int age = CalculateAge(birthDate);
                return age;
            }
            return 0; // Retorna 0 se a data não for válida
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            // Verifica se já fez aniversário este ano
            if (birthDate.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
