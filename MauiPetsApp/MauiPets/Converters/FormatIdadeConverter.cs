using System.Globalization;

namespace MauiPets.Converters
{
    public class FormatIdadeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is int idade)
            {
                return $"({idade} anos)";
            }
            return "(Idade inválida)";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
