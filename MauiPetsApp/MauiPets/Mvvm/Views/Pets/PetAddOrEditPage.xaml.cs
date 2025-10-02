using MauiPets.Mvvm.ViewModels.Pets;

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

    protected override async void OnAppearing()
    {
        await _viewModel.InitializeAsync();
        base.OnAppearing();
    }

    private void GenderType_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            var selectedRadioButton = sender as RadioButton;
            if (selectedRadioButton == genderMale)
                _viewModel.Genero = "M";
            else if (selectedRadioButton == genderFemale)
                _viewModel.Genero = "F";
        }
    }

    private void ImageSelect_Clicked(object sender, EventArgs e)
    {
        // lógica para imagem
    }

    // REMOVIDO:
    // private Picker GetSituationsPicker() { ... }
    // private void SelectedIndexChanged(object sender, EventArgs e) { ... }
}