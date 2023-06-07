namespace DaisyPets.Core.Application.ViewModels
{
    public class RacaoDto
    {
        public int Id { get; set; }
        public int IdPet { get; set; }
        public string DataCompra { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int QuantidadeDiaria { get; set; }
    }
}
