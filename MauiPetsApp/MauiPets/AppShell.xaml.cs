﻿using MauiPets.Mvvm.Views.Contacts;
using MauiPets.Mvvm.Views.Dewormers;
using MauiPets.Mvvm.Views.Expenses;
using MauiPets.Mvvm.Views.Logs;
using MauiPets.Mvvm.Views.PetFood;
using MauiPets.Mvvm.Views.Pets;
using MauiPets.Mvvm.Views.Settings;
using MauiPets.Mvvm.Views.Todo;
using MauiPets.Mvvm.Views.Vaccines;
using MauiPets.Mvvm.Views.VetAppointments;

namespace MauiPets
{
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
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

            Routes.Add(nameof(VaccineAddOrEditPage), typeof(VaccineAddOrEditPage));
            Routes.Add(nameof(DewormerAddOrEditPage), typeof(DewormerAddOrEditPage));
            Routes.Add(nameof(PetFoodAddOrEditPage), typeof(PetFoodAddOrEditPage));
            Routes.Add(nameof(VetAppointmentAddOrEditPage), typeof(VetAppointmentAddOrEditPage));

            Routes.Add(nameof(ExpensesPage), typeof(ExpensesPage));
            Routes.Add(nameof(ExpensesAddOrEditPage), typeof(ExpensesAddOrEditPage));
            Routing.RegisterRoute(nameof(GroupedExpensesPage), typeof(GroupedExpensesPage));

            Routes.Add(nameof(TodoPage), typeof(TodoPage));
            Routes.Add(nameof(TodoAddOrEditPage), typeof(TodoAddOrEditPage));

            Routes.Add(nameof(ContactsPage), typeof(ContactsPage));
            Routes.Add(nameof(ContactDetailPage), typeof(ContactDetailPage));
            Routes.Add(nameof(AddOrEditContactPage), typeof(AddOrEditContactPage));

            Routes.Add(nameof(MainSettingsPage), typeof(MainSettingsPage));
            Routes.Add(nameof(SettingsAddOrEditPage), typeof(SettingsAddOrEditPage));
            Routes.Add(nameof(SettingsManagementPage), typeof(SettingsManagementPage));

            Routes.Add(nameof(LogsMainPage), typeof(LogsMainPage));
            Routes.Add(nameof(LogViewException), typeof(LogViewException));

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}