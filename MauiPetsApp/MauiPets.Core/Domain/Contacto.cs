namespace MauiPetsApp.Core.Domain
{
    public class Contacto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Morada { get; set; } = string.Empty;
        public string Localidade { get; set; } = string.Empty;
        public string Movel { get; set; } = string.Empty;
        public string eMail { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
        public int IdTipoContacto { get; set; }
    }
}
