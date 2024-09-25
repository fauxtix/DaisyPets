using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Application.Interfaces.Services;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Expenses;
public partial class GroupedExpensesViewModel : ObservableObject
{
    private readonly IDespesaService _service;

    public ObservableCollection<GrupoDespesasDto> GroupedExpenses { get; } = new();

    [ObservableProperty]
    private decimal _totalGeralDespesas;

    public GroupedExpensesViewModel(IDespesaService despesaService)
    {
        _service = despesaService;
    }

    [RelayCommand]
    public async Task LoadGroupedExpensesAsync()
    {
        var despesas = await _service.GetAllVMAsync();

        var groupedExpenses = despesas
            .GroupBy(d => d.DescricaoCategoriaDespesa)
            .Select(c => new GrupoDespesasDto
            {
                CategoriaDespesa = c.Key,
                TiposDespesa = c.GroupBy(d => d.DescricaoTipoDespesa)
                    .Select(t => new TipoAgrupadoDto
                    {
                        Descricao = t.Key,
                        Despesas = t.ToList(),
                        SubTotal = t.Sum(d => d.ValorPago)
                    })
                    .ToList(),
                SubTotal = c.Sum(d => d.ValorPago)
            })
            .ToList();

        GroupedExpenses.Clear();
        foreach (var group in groupedExpenses)
        {
            GroupedExpenses.Add(group);
        }

        TotalGeralDespesas = groupedExpenses.Sum(g => g.SubTotal);
    }
}
