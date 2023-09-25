namespace MauiPetsApp.Core.Application.ViewModels
{
    public class ConsultaVeterinarioVM
    {
        public int Id { get; set; }
        public string DataConsulta { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public string Diagnostico { get; set; } = string.Empty;
        public string Tratamento { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;

        public int IdPet { get; set; }
        public string NomePet { get; set; } = string.Empty;
    }
}
