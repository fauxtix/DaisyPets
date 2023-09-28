using MauiPets.Mvvm.Views.Pets;
using MauiPets;
using System.Windows.Input;
using MauiPets.Mvvm.Views.Contacts;

namespace MauiPets
{
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
        public ICommand HelpCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }

        void RegisterRoutes()
        {
            Routes.Add(nameof(MainPage), typeof(MainPage));
            Routes.Add(nameof(PetDetailPage), typeof(PetDetailPage));
            Routes.Add(nameof(PetAddOrEditPage), typeof(PetAddOrEditPage));

            Routes.Add(nameof(ContactsPage), typeof(ContactsPage));
            Routes.Add(nameof(ContactDetailPage), typeof(ContactDetailPage));
            Routes.Add(nameof(AddOrEditContactPage), typeof(AddOrEditContactPage));

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }

        }
    }
}