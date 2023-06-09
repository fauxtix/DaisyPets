namespace DaisyPets.Core.Application.ViewModels.Despesas
{
    public class DespesaVM
    {
        public int Id { get; set; }
        public string DataMovimento { get; set; } = string.Empty;
        public decimal ValorPago { get; set; } = 0;
        public string NumeroDocumento { get; set; } = string.Empty;
        public int IdTipoDespesa { get; set; }
        public string DescricaoTipoDespesa { get; set; } = string.Empty;
        public int IdCategoriaDespesa { get; set; }
        public string DescricaoCategoriaDespesa { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;

    }
}
