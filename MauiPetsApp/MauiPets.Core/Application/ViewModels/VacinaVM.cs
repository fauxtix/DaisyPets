using MauiPetsApp.Core.Application.Formatting;

namespace MauiPetsApp.Core.Application.ViewModels
{
    public class VacinaVM
    {
        public int Id { get; set; }
        public int IdPet { get; set; }
        public int IdTipoVacina { get; set; }
        public string DataToma { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int ProximaTomaEmMeses { get; set; }
        public string NomePet { get; set; } = string.Empty;
        public string NomeTipoVacina { get; set; } = string.Empty;

        public DateTime DataProximaToma
        {
            get
            {
                return !string.IsNullOrEmpty(DataToma) && DataFormat.IsValidDate(DataToma) ?
                    DateTime.Parse(DataToma).AddMonths(ProximaTomaEmMeses) :
                    DateTime.Parse(DataToma.Substring(3, 2) + "/" + DataToma.Substring(0, 2) + DataToma.Substring(5));
            }
        }
        public int DiasParaProximaToma
        {
            get
            {
                return !string.IsNullOrEmpty(DataToma) && DataFormat.IsValidDate(DataToma) ?
                    (int)(DateTime.Parse(DataToma).AddMonths(ProximaTomaEmMeses) - DateTime.Now).TotalDays :
                    (int)(DateTime.Parse(DataToma.Substring(3, 2) + "/" + DataToma.Substring(0, 2) + DataToma.Substring(5)) - DateTime.Now).TotalDays;
            }
        }

        public string DataTomaFormatada
        {
            get
            {
                return DateTime.Parse(DataToma).ToShortDateString();
            }
        }

        public VacinaVM()
        {

        }
    }
}
