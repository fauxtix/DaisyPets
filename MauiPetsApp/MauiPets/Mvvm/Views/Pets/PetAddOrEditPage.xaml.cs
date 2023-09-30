using CommunityToolkit.Maui.Views;
using MauiPets.Mvvm.ViewModels.Pets;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
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

        //if (_viewModel.PetDto is null) return;
        try
        {
            if (BindingContext is PetAddOrEditViewModel viewModel && sender is Picker picker)
            {
                var sit = picker.SelectedItem as LookupTableVM;

                switch (picker)
                {
                    case var _ when picker == SpeciesPicker:
                        viewModel.PetDto.IdEspecie = sit.Id;
                        break;
                    case var _ when picker == TemperamentsPicker:
                        viewModel.PetDto.IdTemperamento = sit.Id;
                        break;
                    case var _ when picker == SituationsPicker:
                        viewModel.PetDto.IdSituacao = sit.Id;
                        break;
                    case var _ when picker == BreedsPicker:
                        viewModel.PetDto.IdRaca = sit.Id;
                        break;
                    case var _ when picker == SizesPicker:
                        viewModel.PetDto.IdTamanho = sit.Id;
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
}