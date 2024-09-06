using CommunityToolkit.Mvvm.ComponentModel;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Settings
{
    public partial class SettingsBaseViewModel : ObservableObject
    {
        public ObservableCollection<LookupTableVM> LookupCollection { get; set; } = new();

        [ObservableProperty]
        private LookupTableVM _lookupRecordSelected;
        [ObservableProperty]
        private string _tableName;
        [ObservableProperty]
        private string _lookupDescription;

        [ObservableProperty] public int _id;
        [ObservableProperty] public string _descricao;

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool _isBusy;
        public bool IsNotBusy => !IsBusy;

        [ObservableProperty]
        private string _editCaption;

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        private string _title;


    }
}
