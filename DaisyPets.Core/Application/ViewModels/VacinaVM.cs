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
        public DateTime DataProximaToma
        {
            get { return !string.IsNullOrEmpty(DataToma) ? DateTime.Parse(DataToma).AddMonths(ProximaTomaEmMeses) : DateTime.Now; }
        }
        public int DiasParaProximaToma
        {
            get { return !string.IsNullOrEmpty(DataToma) ? (int)(DateTime.Parse(DataToma).AddMonths(ProximaTomaEmMeses) - DateTime.Now).TotalDays : 0; }
        }

        public VacinaVM()
        {
        }
    }
}
