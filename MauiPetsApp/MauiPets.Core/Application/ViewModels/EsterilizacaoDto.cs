namespace MauiPetsApp.Core.Application.ViewModels
{
    public class EsterilizacaoDto
    {
        public int Id { get; set; }
        public int IdPet { get; set; }
        public string Veterinario { get; set; } = string.Empty;
    }
}
