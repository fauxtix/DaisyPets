using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Contacts;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;

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
        public double _latitude;

        [ObservableProperty]
        public double _longitude;

        [ObservableProperty]
        public string _contactName;



        [ObservableProperty]
        private string _locationInfo;

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

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        private string _editCaption;


        [RelayCommand]
        private void OnPickerSelected()
        {
            Contacto.IdTipoContacto = SelectedContactType.Id;
            // Outras ações que você deseja executar após a seleção do Picker
        }

        [RelayCommand]
        private async Task AddContactAsync()
        {
            IsEditing = false;
            EditCaption = "Novo contacto";

            ContactoVM contact = new()
            {
                eMail = "",
                IdTipoContacto = 1,
                Localidade = "",
                Morada = "",
                Movel = "",
                Notas = ""
            };

            await Shell.Current.GoToAsync($"{nameof(AddOrEditContactPage)}", true,
                new Dictionary<string, object>
                {
                    {"ContactoVM", contact},
                    {"EditCaption", EditCaption},
                    {"IsEditing", IsEditing },
                });
        }

    }
}
