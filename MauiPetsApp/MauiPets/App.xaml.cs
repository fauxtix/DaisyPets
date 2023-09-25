using MauiPets.Mvvm.ViewModels.Pets;
using MauiPetsApp.Core.Application.Interfaces.Services;
using SQLite;
using System.Reflection;

namespace MauiPets
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //var dbOutput = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PetsDB..db");
            //var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            //using (Stream stream = assembly.GetManifestResourceStream("MauiPets.Database.PetsDB.db"))
            //{
            //    using (MemoryStream memoryStream = new MemoryStream())
            //    {
            //        stream.CopyTo(memoryStream);

            //        File.WriteAllBytes(dbOutput, memoryStream.ToArray());
            //    }
            //}

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