using MauiPetsApp.Core.Application.ViewModels.Despesas;

namespace MauiPets.Core.Application.ViewModels.Despesas
{
    public class GrupoDespesasDto
    {
        public string? CategoriaDespesa { get; set; }
        public List<TipoAgrupadoDto> TiposDespesa { get; set; } = new();
        public decimal SubTotal { get; set; }
    }
}
