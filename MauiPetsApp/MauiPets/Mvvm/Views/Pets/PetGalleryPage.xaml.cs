using MauiPets.Mvvm.ViewModels.Pets;

namespace MauiPets.Mvvm.Views.Pets
{
    public partial class PetGalleryPage : ContentPage
    {
        public PetGalleryPage(PetGalleryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}