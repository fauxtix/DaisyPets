using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Contacts
{
    public partial class BaseViewModel : ObservableObject
    {

        [ObservableProperty]
        public int _id;
        [ObservableProperty]
        public string _nome;
        [ObservableProperty]
        public string _morada;
        [ObservableProperty]
        public string _localidade;
        [ObservableProperty]
        public string _movel;
        [ObservableProperty]
        public string _eMail;
        [ObservableProperty]
        public string _notas;
        [ObservableProperty]
        public int _idTipoContacto;
        [ObservableProperty]
        public string _descricaoTipoContacto;

        [ObservableProperty]
        Contacto contacto = null;

        [ObservableProperty]
        TipoContacto selectedContactType = null;

        [ObservableProperty]
        public ContactoVM _contactoVM = new();


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        public bool IsNotBusy => !IsBusy;

        [RelayCommand]
        private async Task OnPickerSelected()
        {
            Contacto.IdTipoContacto = SelectedContactType.Id;
            // Outras ações que você deseja executar após a seleção do Picker
        }

    }
}
