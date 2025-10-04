namespace MauiPets.Core.Application.ViewModels
{
    public class PetPhoto
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public string PhotoPath { get; set; } = "";
        public DateTime DateAdded { get; set; }
    }
}
