namespace MauiPetsApp.Core.Application.ViewModels
{
    public class ContactoVM
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Morada { get; set; } = string.Empty;
        public string Localidade { get; set; } = string.Empty;
        public string Movel { get; set; } = string.Empty;
        public string eMail { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int IdTipoContacto { get; set; }
        public string DescricaoTipoContacto { get; set; } = string.Empty;
    }
}
