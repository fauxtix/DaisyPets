namespace DaisyPets.Core.Application.ViewModels
{
    public class VacinaVM
    {
        public int Id { get; set; }
        public int IdPet { get; set; }
        public string DataToma { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int ProximaTomaEmMeses { get; set; }
        public string NomePet { get; set; } = string.Empty;
        public DateTime DataProximaToma { get; set; }
        public int DiasParaProximaToma { get; set; }

        public VacinaVM()
        {
            
        }
        public VacinaVM(int id, int idPet, string dataToma, string marca, int proximaTomaEmMeses, string nomePet, DateTime dataProximaToma, int diasproximaToma)
        {
            Id = id;
            IdPet = idPet;
            DataToma = dataToma;
            Marca = marca;
            ProximaTomaEmMeses = proximaTomaEmMeses;
            NomePet = nomePet;
            DataProximaToma = DateTime.Parse(DataToma).AddMonths(ProximaTomaEmMeses);
            DiasParaProximaToma = (int)(DateTime.Parse(DataToma).AddMonths(ProximaTomaEmMeses) - DateTime.Now).TotalDays;
        }
    }
}
