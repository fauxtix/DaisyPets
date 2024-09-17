using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Vaccines;
using MauiPetsApp.Core.Application.Formatting;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPets.Mvvm.ViewModels.Vaccines
{
    public partial class VaccineBaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private int _idPet;
        [ObservableProperty]
        private string _marca;
        [ObservableProperty]
        private string _dataToma;
        [ObservableProperty]
        private int _dataProximaTomaEmMeses;

        [ObservableProperty]
        private DateTime _dataProximaToma;
        [ObservableProperty]
        private int _diasParaProximaToma;

        [ObservableProperty]
        private string _nomePet;

        [ObservableProperty]
        VacinaDto _selectedVaccine;
        [ObservableProperty]
        VacinaVM _selectedVMVaccine;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        public bool IsNotBusy => !IsBusy;

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        private string _addEditCaption;

    }
}
