namespace MauiPetsApp.Core.Application.ViewModels
{
    public class TipoVacinaDto
    {
        public int Id { get; set; }
        public int Id_Especie { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Vacina { get; set; } = string.Empty;
        public string Prevencao { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
    }
}
