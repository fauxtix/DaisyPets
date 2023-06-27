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
        public int DiasParaProximaAplicacao { get; set; }

        public DesparasitanteVM() { }
        public DesparasitanteVM(int id, int idPet, string tipo, string marca, string dataAplicacao, string dataProximaAplicacao, string nomePet, int diasParaProximaAplicacao)
        {
            Id = id;
            IdPet = idPet;
            Tipo = tipo;
            Marca = marca;
            DataAplicacao = dataAplicacao;
            DataProximaAplicacao = dataProximaAplicacao;
            DiasParaProximaAplicacao = (int)(DateTime.Parse(DataProximaAplicacao) - DateTime.Now).TotalDays;
        }
    }
}
