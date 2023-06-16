namespace DaisyPets.Core.Application.ViewModels.Despesas
{
    public class TipoDespesaDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int IdCategoriaDespesa { get; set; }
    }
}
