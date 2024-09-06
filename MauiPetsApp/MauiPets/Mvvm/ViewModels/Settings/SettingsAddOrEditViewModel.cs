using AutoMapper;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Settings;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;

namespace MauiPets.Mvvm.ViewModels.Settings;

[QueryProperty(nameof(LookupRecordSelected), "LookupRecordSelected")]

public partial class SettingsAddOrEditViewModel : SettingsBaseViewModel, IQueryAttributable
{
    private readonly IConnectivity _connectivity;
    private readonly ILookupTableService _lookupTablesService;
    private readonly IMapper _mapper;

    public BackButtonBehavior BackButtonBehavior { get; set; }

    public SettingsAddOrEditViewModel(IConnectivity connectivity, ILookupTableService lookupTablesService, IMapper mapper)
    {
        _connectivity = connectivity;
        _lookupTablesService = lookupTablesService;
        _mapper = mapper;

        BackButtonBehavior = new BackButtonBehavior { IsVisible = false };

    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        EditCaption = query[nameof(EditCaption)] as string;
        IsEditing = (bool)query[nameof(IsEditing)];
        TableName = (string)query[nameof(TableName)];
        Title = (string)query[nameof(Title)];
        LookupRecordSelected = query[nameof(LookupRecordSelected)] as LookupTableVM;

    }

    [RelayCommand]
    async Task SaveLookupData()
    {
        try
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            //var errorMessages = _lookupTablesService..RegistoComErros(DespesaDto);
            //if (!string.IsNullOrEmpty(errorMessages))
            //{
            //    await Shell.Current.DisplayAlert("Verifique entradas, p.f.",
            //        $"{errorMessages}", "OK");
            //    return;
            //}

            if (LookupRecordSelected.Id == 0)
            {
                try
                {
                    var insertedId = await _lookupTablesService.CriaNovoRegisto(LookupRecordSelected);
                    if (insertedId == -1)
                    {
                        await Shell.Current.DisplayAlert("Error while inserting record",
                            $"Please contact administrator..", "OK");
                        return;
                    }

                    await RefreshLookupDataAsync();
                    ShowToastMessage("Registo criado com sucesso");

                    await Shell.Current.GoToAsync($"{nameof(SettingsManagementPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"TableName", TableName},
                        });

                }
                catch (Exception ex)
                {
                    ShowToastMessage($"Erro ao inserir registo {ex.Message}");
                }
            }
            else
            {
                try
                {
                    LookupRecordSelected.Tabela = TableName;
                    await _lookupTablesService.ActualizaDetalhes(LookupRecordSelected);
                    await RefreshLookupDataAsync();
                    ShowToastMessage("Registo atualizado com sucesso");

                    await Shell.Current.GoToAsync($"{nameof(SettingsManagementPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"TableName", TableName},
                        });

                }
                catch (Exception ex)
                {
                    ShowToastMessage($"Erro ao atualizar registo {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            ShowToastMessage($"Erro na transação {ex.Message}");
        }
    }


    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync($"{nameof(SettingsManagementPage)}", true,
            new Dictionary<string, object>
            {
                    {"TableName", TableName},
            });
    }

    private async void ShowToastMessage(string text)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;

        var toast = Toast.Make(text, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }

    private async Task RefreshLookupDataAsync()
    {
        LookupCollection.Clear();
        var lookupData = (await _lookupTablesService.GetLookupTableData(TableName)).ToList();
        var mappedData = _mapper.Map<List<LookupTableVM>>(lookupData);
        foreach (var item in mappedData)
        {
            LookupCollection.Add(item);
        }
    }


}
