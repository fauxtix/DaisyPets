namespace DaisyPets.Core.Application.ViewModels.Despesas
{
    public class TipoDespesaVM
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }
        public int IdCategoriaDespesa { get; set; }
        public string? CategoriaDespesa { get; set; }

    }
}
