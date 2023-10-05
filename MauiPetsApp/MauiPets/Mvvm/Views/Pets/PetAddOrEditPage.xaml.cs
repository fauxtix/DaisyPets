using MauiPets.Mvvm.ViewModels.Pets;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;

namespace MauiPets.Mvvm.Views.Pets;

public partial class PetAddOrEditPage : ContentPage
{
    private PetAddOrEditViewModel _viewModel;

    public PetAddOrEditPage(PetAddOrEditViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private Picker GetSituationsPicker()
    {
        return SituationsPicker;
    }


    private void SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (BindingContext is PetAddOrEditViewModel viewModel && sender is Picker picker)
            {
                var sit = picker.SelectedItem as LookupTableVM;

                //if (_viewModel.Id == 0)
                //{
                //    _viewModel.SpecieSelectedIndex = 0;
                //    _viewModel.BreedSelectedIndex = 2;
                //    _viewModel.SituationSelectedIndex = 3;
                //    _viewModel.TemperamentSelectedIndex = 1;
                //    _viewModel.SizeSelectedIndex = 2;
                //}

                switch (picker)
                {
                    case var _ when picker == SpeciesPicker:
                        _viewModel.PetDto.IdEspecie = sit.Id;
                        break;
                    case var _ when picker == TemperamentsPicker:
                        _viewModel.PetDto.IdTemperamento = sit.Id;
                        break;
                    case var _ when picker == SituationsPicker:
                        _viewModel.PetDto.IdSituacao = sit.Id;
                        break;
                    case var _ when picker == BreedsPicker:
                        _viewModel.PetDto.IdRaca = sit.Id;
                        break;
                    case var _ when picker == SizesPicker:
                        _viewModel.PetDto.IdTamanho = sit.Id;
                        break;
                    default:
                        // Handle any other cases if necessary
                        break;
                }
            }

        }
        catch (Exception ex)
        {
            var msgE = ex.Message;
            throw;
        }
    }

    private async void ImageSelect_Clicked(object sender, EventArgs e)
    {
        //var images = await FilePicker.Default.PickAsync(new PickOptions
        //{
        //    PickerTitle = "Pick Pet Photo",
        //    FileTypes = FilePickerFileType.Images
        //});
        //var imageSource = images.FullPath.ToString();
        //petPhoto.Source = imageSource;
        //outputText.Text = imageSource;
    }
}