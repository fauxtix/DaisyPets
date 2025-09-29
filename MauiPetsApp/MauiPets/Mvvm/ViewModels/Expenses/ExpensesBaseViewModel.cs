using CommunityToolkit.Mvvm.ComponentModel;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Expenses
{
    public partial class ExpensesBaseViewModel : ObservableObject
    {
        // For UI binding, use ObservableCollection
        public ObservableCollection<LookupTableVM> TipoDespesas { get; set; } = new();
        public ObservableCollection<LookupTableVM> CategoriaDespesas { get; } = new();

        // For internal logic, keep List as needed
        public List<TipoDespesaDto> TipoDespesas_Categorias { get; set; } = new();
        public List<LookupTableVM> TipoDespesasFiltradas { get; set; } = new();

        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string dataCriacao = DateTime.Now.ToShortDateString();

        [ObservableProperty]
        private string dataMovimento = DateTime.Now.ToShortDateString();

        [ObservableProperty]
        private decimal valorPago;

        [ObservableProperty]
        private string descricao;

        [ObservableProperty]
        private int idTipoDespesa;

        [ObservableProperty]
        private int idCategoriaDespesa;

        [ObservableProperty]
        private string notas;

        [ObservableProperty]
        private string tipoMovimento = "S";

        [ObservableProperty]
        private DespesaVM despesaVM;

        [ObservableProperty]
        private DespesaDto despesaDto;

        [ObservableProperty]
        private decimal totalDespesas;

        [ObservableProperty]
        private decimal totalGeralDespesas;

        [ObservableProperty]
        private LookupTableVM _tipoDespesaSelecionada;

        [ObservableProperty]
        private LookupTableVM _tipoCategoriaDespesaSelecionada;

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool isBusy;
        public bool IsNotBusy => !isBusy;

        [ObservableProperty]
        private string lookupDescription;

        [ObservableProperty]
        private string editCaption;
    }
}

