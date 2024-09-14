using CommunityToolkit.Mvvm.ComponentModel;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Expenses
{
    public partial class ExpensesBaseViewModel : ObservableObject
    {

        public List<TipoDespesaDto> TipoDespesas_Categorias { get; set; } = new();
        public ObservableCollection<LookupTableVM> TipoDespesas { get; set; } = new();
        public List<LookupTableVM> CategoriaDespesas { get; } = new();
        public List<LookupTableVM> TipoDespesasFiltradas { get; set; } = new();


        [ObservableProperty]
        private LookupTableVM _tipoDespesaSelecionada;
        [ObservableProperty]
        private LookupTableVM _tipoCategoriaDespesaSelecionada;

        [ObservableProperty]
        private int _indiceTipoDespesa;
        [ObservableProperty]
        private int _indiceCategoriaDespesa;


        [ObservableProperty] public int _id;
        [ObservableProperty] public string _dataCriacao;
        [ObservableProperty] public string _dataMovimento;
        [ObservableProperty] public decimal _valorPago;
        [ObservableProperty] public string _descricao;
        [ObservableProperty] public int _idTipoDespesa;
        [ObservableProperty] public int _idCategoriaDespesa;
        [ObservableProperty] public string _notas;
        [ObservableProperty] public string _tipoMovimento = "S";

        [ObservableProperty] public DespesaVM _despesaVM;
        [ObservableProperty] public DespesaDto _despesaDto;

        [ObservableProperty] public decimal _totalDespesas;
        [ObservableProperty] public decimal _totalGeralDespesas;

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        public bool IsNotBusy => !IsBusy;

        [ObservableProperty]
        private string lookupDescription;

        [ObservableProperty]
        private string _editCaption;

    }
}

