using Serilog;
using System.Globalization;

namespace MauiPets.Converters
{
    public class LessThanTotalPagesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (values.Length == 2
              && values[0] is int currentPage
              && values[1] is int totalPages)
                {
                    return currentPage < totalPages;
                }
                return false;

            }
            catch (Exception ex)
            {
                Log.Error($"OnPageSizeChanged: {ex.Message}");
                return false;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
