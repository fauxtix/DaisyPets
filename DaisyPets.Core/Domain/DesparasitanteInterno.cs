namespace DaisyPets.Core.Domain
{
    public class DesparasitanteInterno
    {
        public int Id { get; set; }
        public int IdPet { get; set; }
        public int IdDesparasitanteInterno { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}