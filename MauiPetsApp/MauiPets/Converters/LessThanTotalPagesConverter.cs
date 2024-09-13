using System.Globalization;

namespace MauiPets.Converters
{
    public class LessThanTotalPagesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pageInfo = value as Tuple<int, int>;
            if (pageInfo != null)
            {
                var currentPage = pageInfo.Item1;
                var totalPages = pageInfo.Item2;
                return currentPage < totalPages;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
