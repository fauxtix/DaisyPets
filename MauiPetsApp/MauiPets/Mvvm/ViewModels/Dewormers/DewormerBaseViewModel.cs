using CommunityToolkit.Mvvm.ComponentModel;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPets.Mvvm.ViewModels.Dewormers
{
    public partial class DewormerBaseViewModel : ObservableObject
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
        private DateTime _dataAplicacao;
        [ObservableProperty]
        private DateTime _dataProximaAplicacao;

        [ObservableProperty]
        private string _nomePet;

        [ObservableProperty]
        DesparasitanteDto _selectedDewormer;
        [ObservableProperty]
        DesparasitanteVM _selectedVMDewormer;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        public bool IsNotBusy => !IsBusy;

        [ObservableProperty]
        private bool isEditing;
        [ObservableProperty]
        private bool isTypeInternal;
        [ObservableProperty]
        private bool isTypeExternal;

        [ObservableProperty]
        private List<string> dewormerTypes;

        [ObservableProperty]
        private string _addEditCaption;

        public DewormerBaseViewModel()
        {
            dewormerTypes = new List<string>
            {
                "Interno",
                "Externo"
            };
        }
    }
}
