using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;
using System.Collections.ObjectModel;

namespace MauiPets.Core.Application.ViewModels.Despesas
{
    public class ExpenseCategoryDto
    {
        public LookupTableVM Categoria { get; }
        public ObservableCollection<TipoDespesaDto> TipoDespesas { get; set; } = new();

        public ExpenseCategoryDto(LookupTableVM categoria)
        {
            Categoria = categoria;
        }
    }
}
