﻿using AutoMapper;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Settings;
using MauiPetsApp.Core.Application.Interfaces.Application;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiPets.Mvvm.ViewModels.Settings
{
    [QueryProperty(nameof(TableName), "TableName")]
    public partial class SettingsViewModel : SettingsBaseViewModel, IQueryAttributable
    {
        private readonly ILookupTableRepository _service;
        private readonly IMapper _mapper;

        [ObservableProperty]
        private ObservableCollection<LookupTableVM> filteredLookupCollection;

        [ObservableProperty]
        private string searchText;

        public SettingsViewModel(ILookupTableRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

            LookupCollection = new ObservableCollection<LookupTableVM>();
            FilteredLookupCollection = new ObservableCollection<LookupTableVM>();
        }

        //partial void OnSearchTextChanged(string value)
        //{
        //    FilterLookupCollection();
        //}

        private void FilterLookupCollection()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredLookupCollection = new ObservableCollection<LookupTableVM>(LookupCollection);
            }
            else
            {
                var filteredItems = LookupCollection
                    .Where(item => item.Descricao != null && item.Descricao.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                FilteredLookupCollection = new ObservableCollection<LookupTableVM>(filteredItems);
            }
        }

        [RelayCommand]
        private async Task GetLookupDataAsync()
        {
            try
            {
                if (IsBusy) return;

                IsBusy = true;
                await Task.Delay(100);

                var data = (await _service.GetLookupTableData(TableName)).ToList();
                var mappedData = _mapper.Map<List<LookupTableVM>>(data);

                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    mappedData = mappedData
                        .Where(e =>
                            e.Descricao.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }


                LookupCollection.Clear();

                foreach (var item in mappedData)
                {
                    LookupCollection.Add(item);
                }

                FilterLookupCollection();
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
                        {"Title", Title},
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
            FilterLookupCollection();
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
        private async Task SearchSettingsAsync(string searchText)
        {
            SearchText = searchText;
            await GetLookupDataAsync();
        }


        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync($"//{nameof(MainSettingsPage)}", true);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            TableName = query[nameof(TableName)] as string;
            Title = query[nameof(Title)] as string;
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
}
