namespace MauiPetsApp.Core.Application.ViewModels
{
    public class PetVM
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string DataNascimento { get; set; } = string.Empty;
        public string DoencaCronica { get; set; } = string.Empty;
        public string Observacoes { get; set; } = string.Empty;
        public bool Esterilizado { get; set; }
        public string Chip { get; set; } = string.Empty;
        public bool Chipado { get; set; }
        public int IdPeso { get; set; }
        public bool Padrinho { get; set; }
        public string DataChip { get; set; } = string.Empty;
        public string NumeroChip { get; set; } = string.Empty;
        public string MedicacaoAnimal { get; } = string.Empty;
        public string PesoAnimal { get; set; } = string.Empty;
        public string EspecieAnimal { get; set; } = string.Empty;
        public string TamanhoAnimal { get; set; } = string.Empty;
        public string SituacaoAnimal { get; set; } = string.Empty;
        public string TemperamentoAnimal { get; set; } = string.Empty;
        public string RacaAnimal { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public string Genero { get; set; } = "M";
    }
}
