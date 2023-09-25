namespace MauiPetsApp.Core.Domain
{
    public class TipoDespesa
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int IdCategoriaDespesa { get; set; }
    }
}
