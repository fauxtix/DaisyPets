namespace DaisyPets.Core.Application.ViewModels
{
    public class DesparasitanteExternoDto
    {
        public int Id { get; set; }
        public int IdPet { get; set; }
        public int IdDesparasitanteExterno { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}
