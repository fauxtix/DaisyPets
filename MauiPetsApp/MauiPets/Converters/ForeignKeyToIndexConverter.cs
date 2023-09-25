using MauiPets.Mvvm.ViewModels.Pets;
using System.Globalization;

namespace MauiPets.Converters
{
    public class ForeignKeyToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int foreignKeyId && parameter is string foreignKeyPropertyName)
            {
                // Use DependencyService to create the ViewModel instance with parameters
                var viewModel = DependencyService.Get<PetAddOrEditViewModel>();

                if (viewModel != null)
                {
                    if (foreignKeyPropertyName == "IdEspecie")
                    {
                        var index = viewModel.Species.FindIndex(item => item.Id == foreignKeyId);
                        return index;
                    }
                    else if (foreignKeyPropertyName == "IdSituacao")
                    {
                        var index = viewModel.Situations.FindIndex(item => item.Id == foreignKeyId);
                        return index;
                    }
                    else if (foreignKeyPropertyName == "IdRaca")
                    {
                        var index = viewModel.Breeds.FindIndex(item => item.Id == foreignKeyId);
                        return index;
                    }
                    else if (foreignKeyPropertyName == "IdTamanho")
                    {
                        var index = viewModel.Sizes.FindIndex(item => item.Id == foreignKeyId);
                        return index;
                    }
                    else if (foreignKeyPropertyName == "IdTemperamento")
                    {
                        var index = viewModel.Situations.FindIndex(item => item.Id == foreignKeyId);
                        return index;
                    }
                    // Add more conditions for other foreign keys as needed
                }
            }

            return -1; // Return -1 or handle the case where the foreign key is not found
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
