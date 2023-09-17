using CommunityToolkit.Mvvm.ComponentModel;
using MauiPets.Mvvm.Models;
using MauiPets.Mvvm.Views;

namespace MauiPets.Mvvm.ViewModels.Pets
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        public string _id;
        [ObservableProperty]
        private string _nome;

        [ObservableProperty]
        private int _idEspecie;
        [ObservableProperty]
        private int _idRaca;
        [ObservableProperty]
        private int _idTamanho;
        [ObservableProperty]
        private int _idSituacao;
        [ObservableProperty]
        private int _idPeso;
        [ObservableProperty]
        private int _IdTemperamento;
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
        private string _dataNascimento;
        [ObservableProperty]
        private string _doencaCronica;
        [ObservableProperty]
        private string _genero;
        [ObservableProperty]
        private string _dataChip;
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
        public PetVM _pet;

        [ObservableProperty]
        public PetDto _petDto;

        [ObservableProperty]
        public LookupTableVM _lookupTable;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        public bool IsNotBusy => !IsBusy;
    }
}
