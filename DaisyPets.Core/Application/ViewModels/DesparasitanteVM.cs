namespace DaisyPets.Core.Application.ViewModels
{
    public class DesparasitanteVM
    {

        public int Id { get; set; }
        public int IdPet { get; set; }
        public string Tipo { get; set; } = string.Empty; // "I" - Interno - "E" - Externo
        public string Marca { get; set; } = string.Empty;
        public string DataAplicacao { get; set; } = string.Empty;
        public string DataProximaAplicacao { get; set; } = string.Empty;
        public string NomePet { get; set; } = string.Empty;
    }
}
