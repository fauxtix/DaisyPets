namespace DaisyPets.Core.Domain
{
    public class ConsultaVeterinario
    {
        public int Id { get; set; }
        public string DataConsulta { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public string Diagnostico { get; set; } = string.Empty;
        public string Tratamento { get; set; } = String.Empty;
        public int IdPet { get; set; }
    }
}
