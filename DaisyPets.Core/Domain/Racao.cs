namespace DaisyPets.Core.Domain
{
    public class Racao
    {
        public int Id { get; set; }
        public int IdPet { get; set; }
        public string DataCompra { get; set; } = string.Empty;
        public int IdMarca { get; set; }
        public int QuantidadeDiaria { get; set; }
    }
}
