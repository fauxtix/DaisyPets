using AutoMapper;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Expenses;
using MauiPetsApp.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.Despesas;

namespace MauiPets.Mvvm.ViewModels.Expenses;

[QueryProperty(nameof(DespesaDto), "DespesaDto")]

public partial class ExpenseAddOrEditViewModel : ExpensesBaseViewModel, IQueryAttributable
{

    private readonly IDespesaService _service;
    private readonly ITipoDespesaService _tipoDespesaService;
    private readonly ILookupTableService _lookupTablesService;
    private readonly IMapper _mapper;

    public ExpenseAddOrEditViewModel(IDespesaService service,
                                     ILookupTableService lookupTablesService,
                                     IMapper mapper,
                                     ITipoDespesaService tipoDespesaService)
    {
        _service = service;
        _lookupTablesService = lookupTablesService;

        SetupLookupTables();
        _mapper = mapper;
        _tipoDespesaService = tipoDespesaService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        EditCaption = query[nameof(EditCaption)] as string;
        IsEditing = (bool)query[nameof(IsEditing)];

        DespesaDto = query[nameof(DespesaDto)] as DespesaDto;

        if (DespesaDto is null)
        {
            IndiceCategoriaDespesa = 0;
            IndiceTipoDespesa = 0;
        }
        else
        {
            var idxCategoria = DespesaDto.IdCategoriaDespesa;
            var idxTipo = DespesaDto.IdTipoDespesa;
            IndiceCategoriaDespesa = CategoriaDespesas.FindIndex(cd => cd.Id == idxCategoria);
            IndiceTipoDespesa = TipoDespesasFiltradas.FindIndex(td => td.Id == idxTipo);
        }
    }

    private void SetupLookupTables()
    {
        IsBusy = true;

        GetLookupData("CategoriaDespesa");
        GetLookupData("TipoDespesa");

        IsBusy = false;

    }

    public async void GetLookupData(string tableName)
    {
        IsBusy = true;
        try
        {
            var result = (await _lookupTablesService.GetLookupTableData(tableName)).ToList();
            if (result is null)
            {
                IsBusy = false;
                return;
            }

            if (tableName.ToLower() == "categoriadespesa")
            {
                CategoriaDespesas.Clear();
                CategoriaDespesas.AddRange(result);
            }
            else
            {
                foreach (var item in result)
                {
                    TipoDespesas.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error while 'GetLookupData", ex.Message, "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task SaveExpenseData()
    {

        try
        {
            IsBusy = true;
            await Task.Delay(100);
            var errorMessages = _service.RegistoComErros(DespesaDto);
            if (!string.IsNullOrEmpty(errorMessages))
            {
                await Shell.Current.DisplayAlert("Verifique entradas, p.f.",
                    $"{errorMessages}", "OK");
                return;
            }

            if (DespesaDto.Id == 0)
            {
                try
                {
                    var insertedId = await _service.InsertAsync(DespesaDto);
                    if (insertedId == -1)
                    {
                        await Shell.Current.DisplayAlert("Error while inserting expense",
                            $"Please contact administrator..", "OK");
                        return;
                    }

                    ShowToastMessage("Despesa criada com sucesso");

                    var expenseVM = _mapper.Map<DespesaVM>(await _service.GetVMByIdAsync(insertedId));

                    await Shell.Current.GoToAsync($"//{nameof(ExpensesPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"DespesaVM", expenseVM}
                        });

                }
                catch (Exception ex)
                {
                    ShowToastMessage($"Erro ao inserir despesa {ex.Message}");
                }
                finally
                {
                    IsBusy = false;
                }

            }
            else
            {
                try
                {
                    await Task.Delay(200);
                    await _service.UpdateAsync(DespesaDto.Id, DespesaDto);
                    ShowToastMessage("Registo atualizado com sucesso");


                    await Shell.Current.GoToAsync($"//{nameof(ExpensesPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"DespesaDto", DespesaDto}
                        });
                }
                catch (Exception ex)
                {
                    ShowToastMessage($"Erro ao atualizar registo {ex.Message}");
                }
                finally
                {
                    IsBusy = false;
                }

            }
        }
        catch (Exception ex)
        {
            ShowToastMessage($"Erro na transação {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task GoBack()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync($"//{nameof(ExpensesPage)}");

        }
        finally
        {
            IsBusy = false;
        }
    }

    private async void ShowToastMessage(string text)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;

        var toast = Toast.Make(text, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }
}
