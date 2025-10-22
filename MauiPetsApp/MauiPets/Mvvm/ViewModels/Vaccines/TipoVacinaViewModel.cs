using CommunityToolkit.Mvvm.ComponentModel;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Vaccines;

public partial class TipoVacinasViewModel : ObservableObject
{
    private readonly IVacinasService _tipoVacinaService;

    [ObservableProperty]
    private ObservableCollection<TipoVacinaDto> _tipoVacinas = new();

    [ObservableProperty]
    private bool _isBusy;

    public TipoVacinasViewModel(IVacinasService tipoVacinaService)
    {
        _tipoVacinaService = tipoVacinaService;
        _ = LoadVacinasAsync();
    }

    public async Task LoadVacinasAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var tipoVacinasList = (await _tipoVacinaService.GetTipoVacinasAsync(1)).ToList();
            TipoVacinas.Clear();
            foreach (var vaccine in tipoVacinasList)
            {
                TipoVacinas.Add(vaccine);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }
}
