using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Behaviours.Pickers;
using MauiPets.Mvvm.Models;
using MauiPets.Mvvm.ViewModels.Pets;
using System.Diagnostics;
using System.Windows.Input;

namespace MauiPets.Mvvm.Views.Pets;

public partial class PetAddOrEditPage : ContentPage
{
    private PetAddOrEditViewModel viewModel;
    public PetAddOrEditPage(PetAddOrEditViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;

        RegisterPickers();
        BindingContext =this.viewModel;


        //(BindingContext as PetAddOrEditViewModel).PropertyChanged += (sender, args) =>
        //{
        //    if (args.PropertyName == nameof(PetAddOrEditViewModel.SelectedSpecie))
        //    {
        //        var selectedSpecieId = (BindingContext as PetAddOrEditViewModel).SelectedSpecie.Id;
        //        (BindingContext as PetAddOrEditViewModel).PetDto.IdEspecie = selectedSpecieId;
        //    }
        //};
    }

    //private async void SaveProductButton_OnClicked(object sender, EventArgs e)
    //{
    //    bool answer = await DisplayAlert("Gravar dados do Pet", "Confirma operação", "Sim", "Não");
    //    Debug.WriteLine("Answer: " + answer);
    //}

    private void RegisterPickers()
    {
        this.viewModel.RegisterPicker(SpeciesPicker, selectedItem => HandlePickerSelection(SpeciesPicker, selectedItem));
        this.viewModel.RegisterPicker(TemperamentsPicker, selectedItem => HandlePickerSelection(TemperamentsPicker, selectedItem));
        this.viewModel.RegisterPicker(SituationsPicker, selectedItem => HandlePickerSelection(SituationsPicker, selectedItem));
    }
    private void HandlePickerSelection(Picker picker, object selectedItem)
    {
        if (selectedItem != null)
        {
            switch (picker)
            {
                case Picker speciesPicker when speciesPicker == SpeciesPicker:
                    var selectedSpecie = (LookupTableVM)selectedItem;
                    int selectedSpecieId = selectedSpecie.Id;
                    this.viewModel.IdEspecie = selectedSpecieId;
                    break;
                case Picker temperamentsPicker when temperamentsPicker == TemperamentsPicker:
                    var selectedTemperament = (LookupTableVM)selectedItem;
                    int selectedTemperamentId = selectedTemperament.Id;
                    this.viewModel.IdTemperamento = selectedTemperamentId;
                    break;
                case Picker situationsPicker when situationsPicker == SituationsPicker:
                    var selectedSituation = (LookupTableVM)selectedItem;
                    int selectedSituationId = selectedSituation.Id;
                    this.viewModel.IdSituacao = selectedSituationId;
                    break;
            }
        }
    }

}