using CommunityToolkit.Mvvm.ComponentModel; // Importando o Community Toolkit
using MauiPets.Mvvm.Models;
using MauiPets.Mvvm.Views.Settings;
using MauiPets.Mvvm.Views.Settings.Expenses;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Settings
{
    public partial class SettingsPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ContentPage _currentPage;

        public ObservableCollection<SettingsPageModel> Pages { get; } = new ObservableCollection<SettingsPageModel>();

        public SettingsPageViewModel(IServiceProvider serviceProvider)
        {

            var mainSettingsPage = serviceProvider.GetRequiredService<MainSettingsPage>();
            var expenseSettingsPage = serviceProvider.GetRequiredService<ExpenseSettingsPage>();

            if (mainSettingsPage == null)
            {
                throw new Exception("MainSettingsPage não pôde ser resolvido");
            }

            if (expenseSettingsPage == null)
            {
                throw new Exception("ExpenseSettingsPage não pôde ser resolvido");
            }

            // Adicionando as páginas ao ObservableCollection
            Pages.Add(new SettingsPageModel
            {
                Title = "Main Settings",
                Page = mainSettingsPage
            });
            Pages.Add(new SettingsPageModel
            {
                Title = "Expense Settings",
                Page = expenseSettingsPage
            });

            // Definindo a página atual como a primeira página
            if (Pages.Count > 0)
            {
                CurrentPage = Pages[0].Page; // Garantindo que há pelo menos uma página
            }
            else
            {
                CurrentPage = null; // Evitando erro de index
            }
        }
    }
}
