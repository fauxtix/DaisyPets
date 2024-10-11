using CommunityToolkit.Mvvm.ComponentModel;
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
        private string _dataToma = DateTime.Now.ToShortDateString();

        [ObservableProperty]
        private int _dataProximaTomaEmMeses;

        [ObservableProperty]
        private DateTime _dataProximaToma = DateTime.Now.AddDays(1);

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
