using CommunityToolkit.Mvvm.ComponentModel;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;

namespace MauiPets.Mvvm.ViewModels.Pets
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        public int _id;
        [ObservableProperty]
        private string _nome;

        [ObservableProperty]
        private int _idEspecie;
        [ObservableProperty]
        private int _idSituacao;
        [ObservableProperty]
        private int _idTemperamento;
        [ObservableProperty]
        private int _idRaca;
        [ObservableProperty]
        private int _idTamanho;
        [ObservableProperty]
        private int _idPeso;

        [ObservableProperty]
        private int _chipado;
        [ObservableProperty]
        private int _esterilizado;
        [ObservableProperty]
        private int _padrinho;
        [ObservableProperty]
        private string _chip;
        [ObservableProperty]
        private string _observacoes;
        [ObservableProperty]
        private string _dataNascimento = DateTime.Now.ToShortDateString();
        [ObservableProperty]
        private string _doencaCronica;
        [ObservableProperty]
        private string _medicacao;
        [ObservableProperty]
        private string _cor;

        [ObservableProperty]
        private string _genero;

        [ObservableProperty]
        public string _gender;
        [ObservableProperty]
        public string _godFather;
        [ObservableProperty]
        public string _sterilized;
        [ObservableProperty]
        public string _chipped;


        [ObservableProperty]
        private string _dataChip = DateTime.Now.ToShortDateString();
        [ObservableProperty]
        private string _numeroChip;
        [ObservableProperty]
        private string _medicacaoAnimal;
        [ObservableProperty]
        private string _racaAnimal;
        [ObservableProperty]
        private string _temperamentoAnimal;
        [ObservableProperty]
        public string _foto;

        [ObservableProperty]

        private string _petPhoto;

        [ObservableProperty]
        public PetVM _pet = new();

        [ObservableProperty]
        public VacinaDto _selectedVaccine = new();
        [ObservableProperty]
        public DesparasitanteDto _selectedDewormer = new();
        [ObservableProperty]
        public RacaoDto _selectedPetFood = new();
        [ObservableProperty]
        public ConsultaVeterinarioDto _selectedAppointment = new();


        [ObservableProperty]
        public PetDto _petDto = new();

        [ObservableProperty]
        public LookupTableVM _lookupTableVM;

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
