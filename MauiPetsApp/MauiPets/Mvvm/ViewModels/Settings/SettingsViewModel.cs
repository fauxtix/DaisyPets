using AutoMapper;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Extensions;
using MauiPets.Mvvm.Views.Settings;
using MauiPetsApp.Core.Application.Interfaces.Application;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;
using System.Diagnostics;

namespace MauiPets.Mvvm.ViewModels.Settings
{
    [QueryProperty(nameof(TableName), "TableName")]

    public partial class SettingsViewModel : SettingsBaseViewModel, IQueryAttributable
    {
        private readonly ILookupTableRepository _service;
        private readonly IConnectivity _connectivity;
        private readonly IMapper _mapper;

        public SettingsViewModel(ILookupTableRepository service, IConnectivity connectivity, IMapper mapper)
        {
            _service = service;
            _connectivity = connectivity;
            _mapper = mapper;
        }

        [RelayCommand]
        private async Task AddLookupTableDataAsync()
        {
            EditCaption = "Novo registo";
            IsEditing = false;

            LookupRecordSelected = new()
            {
                Descricao = "", 
                Tabela = TableName                
            };

            await NavigateToEditPage(LookupRecordSelected);
        }

        [RelayCommand]
        private async Task EditLookupRecordAsync(LookupTableVM record)
        {
            if (record?.Id > 0)
            {
                IsEditing = false;
                EditCaption = "Editar";

                var response = await _service.GetRecordById(record.Id, TableName);
                if (response != null)
                {
                    var mappedRecord = _mapper.Map<LookupTableVM>(response);
                    await NavigateToEditPage(mappedRecord);
                }
            }
        }



        [RelayCommand]
        private async Task GetLookupDataAsync()
        {
            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                if (IsBusy)
                    return;

                IsBusy = true;

                var data = (await _service.GetLookupTableData(TableName)).ToList();
                var mappedData = _mapper.Map<List<LookupTableVM>>(data);
                if (data.Count != 0)
                {
                    LookupCollection.Clear();
                }

                foreach (var item in mappedData)
                {
                    LookupCollection.Add(item);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get data: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        private async Task GetPetAsync()
        {
            var petId = Id;
            if (petId > 0)
            {
                var source = await _service.GetRecordById(LookupRecordSelected.Id, TableName);
                LookupTableVM destination = new LookupTableVM();
                if (source is not null)
                {
                    LookupRecordSelected = _mapper.Map<LookupTableVM>(source);
                    await NavigateToEditPage(LookupRecordSelected);
                }
            }
        }

        [RelayCommand]
        private async Task DeleteLookupRecordAsync(LookupTableVM recordToDelete)
        {
            if (recordToDelete == null) return;

            bool okToDelete = await Shell.Current.DisplayAlert("Confirme, por favor", $"Apagar a entrada {recordToDelete.Descricao}?", "Sim", "Não");
            if (okToDelete)
            {
                IsBusy = true;
                await _service.DeleteRegisto(recordToDelete.Id, TableName);
                await RefreshLookupDataAsync();
                ShowToastMessage($"Entrada {recordToDelete.Descricao} apagada com sucesso");

                await Shell.Current.GoToAsync($"{nameof(SettingsManagementPage)}", true,
                    new Dictionary<string, object>
                    {
                            {"TableName", TableName},
                    });
            }
            IsBusy = false;
        }

        private async Task RefreshLookupDataAsync()
        {
            LookupCollection.Clear();
            var lookupData = (await _service.GetLookupTableData(TableName)).ToList();
            var mappedData = _mapper.Map<List<LookupTableVM>>(lookupData);
            foreach (var item in mappedData)
            {
                LookupCollection.Add(item);
            }
        }

        private async Task NavigateToEditPage(LookupTableVM lookupDto)
        {
            await Shell.Current.GoToAsync($"{nameof(SettingsAddOrEditPage)}", true,
                new Dictionary<string, object>
                {
                    {"LookupRecordSelected", lookupDto},
                    {"EditCaption", EditCaption},
                    {"IsEditing", IsEditing },
                    {"TableName", TableName},
                    {"Title", Title},
                });
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync($"//{nameof(MainSettingsPage)}", true);
        }

        private async void ShowToastMessage(string text)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            TableName = query[nameof(TableName)] as string;
            Title= query[nameof(Title)] as string;
        }
    }
}
