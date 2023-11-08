using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Vaccines;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.Interfaces.Services.TodoManager;
using MauiPetsApp.Core.Application.TodoManager;
using MauiPetsApp.Core.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiPets.Mvvm.ViewModels.Vaccines
{
    public partial class VaccineAddOrEditModel : VaccineBaseViewModel, IQueryAttributable
    {
        public IVacinasService _vaccinesService { get; set; }
        public int SelectedTodoId { get; set; }
        public IConnectivity _connectivity;

        public VaccineAddOrEditModel(IVacinasService vacinnesService, IConnectivity connectivity)
        {
            _vaccinesService = vacinnesService;
            _connectivity = connectivity;
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            SelectedVaccine = query[nameof(SelectedVaccine)] as VacinaDto;
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync($"//{nameof(VaccinesPage)}");
        }

        [RelayCommand]
        async Task SaveTodo()
        {
            try
            {
                //if(IsNotBusy)
                //    IsBusy = true;

                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                if (SelectedVaccine.Id == 0)
                {
                    var insertedId = await _vaccinesService.InsertAsync(SelectedVaccine);
                    if (insertedId == -1)
                    {
                        await Shell.Current.DisplayAlert("Error while updating",
                            $"Please contact administrator..", "OK");
                        return;
                    }

                    var vaccineDto = await _vaccinesService.GetPetVaccinesVMAsync(insertedId);
                    //IsBusy = false;

                    ShowToastMessage("Contact created succesfuly");

                    await Shell.Current.GoToAsync($"//{nameof(VaccinesPage)}", true,
                        new Dictionary<string, object>
                        {
                        {"SelectedVaccine", vaccineDto}
                        });


                }
                else // Insert (Id > 0)
                {
                    var _vaccineId = SelectedVaccine.Id;
                    await _vaccinesService.UpdateAsync(_vaccineId, SelectedVaccine);

                    var vaccineDto = await _vaccinesService.GetPetVaccinesVMAsync(_vaccineId);

                    await Shell.Current.GoToAsync($"//{nameof(VaccinesPage)}", true,
                        new Dictionary<string, object>
                        {
                        {"SelectedVaccine", vaccineDto}
                        });

                    //IsBusy = false;
                    ShowToastMessage("Record updated successfuly");

                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                ShowToastMessage($"Error while creating Vaccine ({ex.Message})");
            }
        }

        private async void ShowToastMessage(string text)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }


    }
}
