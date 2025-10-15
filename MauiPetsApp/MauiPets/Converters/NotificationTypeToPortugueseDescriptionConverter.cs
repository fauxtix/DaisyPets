using System.Globalization;

namespace MauiPets.Converters
{
    public class NotificationTypeToPortugueseDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var desc = value?.ToString()?.ToLower();
            return desc switch
            {
                "vet_appointment" => "Consulta veterinária",
                "dewormer" => "Desparasitante",
                "vaccine" => "Vacina",
                "task" => "Tarefa",
                _ => value?.ToString()
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
