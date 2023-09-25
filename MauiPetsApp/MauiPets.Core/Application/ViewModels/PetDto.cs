namespace MauiPetsApp.Core.Application.ViewModels
{
    public class PetDto
    {
        public int Id { get; set; }
        public int IdEspecie { get; set; }
        public int IdTemperamento { get; set; }
        public int IdSituacao { get; set; }
        public int IdRaca { get; set; }
        public int IdTamanho { get; set; }
        public string DataNascimento { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public string Cor { get; set; } = string.Empty;
        public string Genero { get; set; } = "M";
        public int IdPeso { get; set; }
        public string Medicacao { get; set; } = string.Empty;
        public int Chipado { get; set; }
        public string Chip { get; set; } = string.Empty;
        public string DataChip { get; set; } = string.Empty;
        public string NumeroChip { get; set; } = string.Empty;

        public int Esterilizado { get; set; }
        public int Padrinho { get; set; }
        public string DoencaCronica { get; set; } = string.Empty;
        public string Observacoes { get; set; } = string.Empty;
    }
}
