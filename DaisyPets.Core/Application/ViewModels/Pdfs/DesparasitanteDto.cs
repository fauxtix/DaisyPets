namespace DaisyPets.Core.Application.ViewModels.Pdfs
{
    public class DesparasitanteDto
    {
        public int Id { get; set; }
        public int IdPet { get; set; }
        public string Tipo { get; set; } = string.Empty; // "I" - Interno - "E" - Externo
        public string Marca { get; set; } = string.Empty;
        public string DataAplicacao { get; set; } = string.Empty;
        public string DataProximaAplicacao { get; set; } = string.Empty;

    }
}
