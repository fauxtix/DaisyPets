using CommunityToolkit.Mvvm.ComponentModel;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPets.Mvvm.ViewModels.PetFood
{
    public partial class PetFoodBaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private int _idPet;
        [ObservableProperty]
        private string _marca;
        [ObservableProperty]
        private string _tipo;
        [ObservableProperty]
        private DateTime _dataCompra;
        [ObservableProperty]
        private int _quantidadeDiaria;

        [ObservableProperty]
        private string _nomePet;

        [ObservableProperty]
        RacaoDto _selectedPetFood;
        [ObservableProperty]
        RacaoVM _selectedPetFoodVM;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        public bool IsNotBusy => !IsBusy;

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        private string _editCaption;
    }
}
