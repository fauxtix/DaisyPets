using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPetsApp.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Settings
{
    public partial class ExpensesSettingsViewModel : ObservableObject
    {
        private readonly IDespesaService _service;
        private readonly ITipoDespesaService _tipoDespesaService;
        private readonly ILookupTableService _lookupTablesService;
        private readonly IMapper _mapper;

        public ObservableCollection<TipoDespesaDto> TipoDespesas { get; set; } = new();
        public List<LookupTableVM> CategoriaDespesas { get; } = new();
        public List<TipoDespesaDto> TipoDespesasFiltradas { get; set; } = new();

        [ObservableProperty] public DespesaVM _despesaVM;
        [ObservableProperty] public DespesaDto _despesaDto;



        [ObservableProperty]
        private LookupTableVM _tipoDespesaSelecionada;
        [ObservableProperty]
        private LookupTableVM _tipoCategoriaDespesaSelecionada;

        [ObservableProperty]
        private int _indiceTipoDespesa;
        [ObservableProperty]
        private int _indiceCategoriaDespesa;

        [ObservableProperty]
        private bool isEditing;
        [ObservableProperty]
        private bool expand = false;


        public ExpensesSettingsViewModel(IDespesaService service,
            ITipoDespesaService tipoDespesaService,
            ILookupTableService lookupTablesService,
            IMapper mapper)
        {
            _service = service;
            _tipoDespesaService = tipoDespesaService;
            _lookupTablesService = lookupTablesService;
            _mapper = mapper;

            SetupLookupTables();
        }

        private void SetupLookupTables()
        {
            GetLookupData("CategoriaDespesa");
            // GetLookupData("TipoDespesa");
        }

        [RelayCommand]
        public async Task GetTiposDespesaAsync(LookupTableVM tableVM)
        {
            var id = tableVM.Id;
            var result = (await _tipoDespesaService.GetTipoDespesa_ByCategoria(id)).ToList();
            TipoDespesas.Clear();
            foreach (var item in result)
            {
                TipoDespesas.Add(item);
            }

            Expand = !Expand;
        }
        public async void GetLookupData(string tableName)
        {
            try
            {
                var result = (await _lookupTablesService.GetLookupTableData(tableName)).ToList();
                if (result is null)
                {
                    return;
                }

                if (tableName.ToLower() == "categoriadespesa")
                {
                    CategoriaDespesas.AddRange(result);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error while 'GetLookupData", ex.Message, "Ok");
            }
            finally
            {
            }
        }



    }
}
