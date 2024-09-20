using MauiPetsApp.Core.Application.ViewModels.Despesas;

namespace MauiPets.Core.Application.ViewModels.Despesas
{
    public class TipoAgrupadoDto
    {
        public string Descricao { get; set; } = string.Empty;
        public List<DespesaVM> Despesas { get; set; } = new();
        public decimal SubTotal { get; set; }
    }
}
