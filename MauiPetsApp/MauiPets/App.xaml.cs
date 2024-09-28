using MauiPets.Mvvm.ViewModels.Pets;
using MauiPetsApp.Core.Application.Interfaces.Services;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;

namespace MauiPets
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            LocalNotificationCenter.Current.NotificationActionTapped += OnNotificationActionTapped;
            MainPage = new AppShell();
        }


        private void OnNotificationActionTapped(NotificationActionEventArgs e)
        {
            if (e.IsDismissed)
            {
                // your code goes here
                return;
            }
            if (e.IsTapped)
            {
                // your code goes here
                return;
            }
            // if Notification Action are setup
            //switch (e.ActionId)
            //{

            //}
        }

        public static class ViewModelFactory
        {
            public static PetAddOrEditViewModel CreatePetAddOrEditViewModel()
            {
                var petService = DependencyService.Get<IPetService>();
                var lookupTablesService = DependencyService.Get<ILookupTableService>();

                return new PetAddOrEditViewModel(petService, lookupTablesService);
            }
        }
    }
}