using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Expenses;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;

namespace MauiPets.Mvvm.ViewModels.Expenses
{
    public partial class ExpensesBaseViewModel : ObservableObject
    {

        public List<TipoDespesaDto> TipoDespesas_Categorias { get; set; } = new();
        public List<LookupTableVM> TipoDespesas { get; set; } = new();
        public List<LookupTableVM> CategoriaDespesas { get; } = new();

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

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        public bool IsNotBusy => !IsBusy;

        [ObservableProperty]
        private string lookupDescription;

        [RelayCommand]
        private async Task AddExpenseAsync()
        {
            DespesaDto = new()
            {
                DataCriacao = DateTime.Now.Date.ToShortDateString(),
                DataMovimento = DateTime.Now.Date.ToShortDateString(),
                ValorPago = 0M,
                IdCategoriaDespesa = 0,
                IdTipoDespesa = 0,
                Descricao = "",
                Notas = "",
                TipoMovimento = ""
            };

            await Shell.Current.GoToAsync($"{nameof(ExpensesAddOrEditPage)}", true,
                new Dictionary<string, object>
                {
                        {"DespesaDto", DespesaDto}
                });
        }
    }
}

