namespace DaisyPets.Core.Domain
{
    public class Vacina
    {
        public int Id { get; set; }
        public int IdPet { get; set; }
        public string DataToma { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int ProximaTomaEmMeses { get; set; }
    }
}
