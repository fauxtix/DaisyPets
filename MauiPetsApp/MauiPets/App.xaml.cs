using MauiPets.Mvvm.ViewModels.Pets;
using MauiPetsApp.Core.Application.Interfaces.Services;

namespace MauiPets
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {

            MainPage = new AppShell();
        }

        public static class ViewModelFactory
        {
            public static PetAddOrEditViewModel CreatePetAddOrEditViewModel()
            {
                var connectivity = DependencyService.Get<IConnectivity>();
                var petService = DependencyService.Get<IPetService>();
                var lookupTablesService = DependencyService.Get<ILookupTableService>();

                return new PetAddOrEditViewModel(connectivity, petService, lookupTablesService);
            }
        }
    }
}