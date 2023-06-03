namespace DaisyPets.Core.Domain
{
    public class Esterilizacao
    {
        public int Id { get; set; }
        public int IdPet { get; set; }
        public string Veterinario { get; set; } = string.Empty;
    }
}
