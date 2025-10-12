using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MauiPets.Core.Application.Interfaces.Services.Notifications;
using MauiPets.Core.Application.ViewModels.Messages;
using MauiPets.Extensions;
using MauiPets.Mvvm.Views.Pets;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Pets;

public partial class PetViewModel : BaseViewModel
{
    private readonly ILogger<PetViewModel> _logger;

    public ObservableCollection<PetVM> Pets { get; } = new();

    private readonly IPetService _petService;
    private readonly IVacinasService _petVaccinesService;
    private readonly INotificationsSyncService? _notificationService;

    public PetViewModel(IPetService petService,
                        IVacinasService petVaccinesService,
                        ILogger<PetViewModel> logger,
                        INotificationsSyncService? notificationService = null)
    {
        _petService = petService;
        _petVaccinesService = petVaccinesService;
        _logger = logger;
        _notificationService = notificationService;
        Task.Run(GetPetsAsync);
        Task.Run(UpdateUnreadNotificationsAsync); // Atualiza badge ao iniciar
        WeakReferenceMessenger.Default.Register<UpdateUnreadNotificationsMessage>(this, async (r, m) =>
        {
            await UpdateUnreadNotificationsAsync();
        });
    }

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty] string shareStatus;

    // === PROPRIEDADES DO BADGE ===
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasUnreadNotifications))]
    int unreadNotificationsCount;

    public bool HasUnreadNotifications => UnreadNotificationsCount > 0;

    [RelayCommand]
    private async Task OpenNotificationsAsync()
    {
        await Shell.Current.GoToAsync("NotificationsPage");
    }

    public async Task UpdateUnreadNotificationsAsync()
    {
        if (_notificationService is not null)
        {
            try
            {
                UnreadNotificationsCount = await _notificationService.GetActiveNotificationsCountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter notificações: {ex.Message}");
            }
        }
        else
        {
            UnreadNotificationsCount = 0;
        }
    }

    [RelayCommand]
    private async Task GetPetsAsync()
    {
        try
        {
            if (IsBusy)
                return;

            IsBusy = true;
            await Task.Yield();

            var pets = (await _petService.GetAllVMAsync()).ToList();

            if (pets.Count > 0)
            {
                Pets.Clear();
                Pets.AddRange(pets);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao ler ficheiro de Pets. {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async Task GetPetAsync()
    {
        var petId = Id;
        if (petId > 0)
        {
            var response = await _petService.GetPetVMAsync(petId);

            if (response is not null)
            {
                Pet = response;

                await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                            {"PetVM", Pet },
                    });

            }
        }
    }

    [RelayCommand]
    private async Task DisplayPetAsync(PetVM petVM)
    {
        if (petVM is null)
        {
            return;
        }

        try
        {
            IsBusy = true;
            await Task.Yield();
            await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
                new Dictionary<string, object>
                {
                    {"PetVM", petVM },
                 });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao abrir detalhe do Pet. {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task AddPetAsync()
    {
        EditCaption = "Novo registo";
        IsEditing = false;

        PetDto = new PetDto()
        {
            Chip = "",
            Chipado = 0,
            Cor = "",
            DataChip = DateTime.Today.Date.ToString("yyyy-MM-dd"),
            DataNascimento = DateTime.Today.Date.ToString("yyyy-MM-dd"),
            DoencaCronica = "",
            Esterilizado = 1,
            Genero = "M",
            Medicacao = "",
            Nome = "",
            NumeroChip = "",
            Observacoes = "",
            Padrinho = 1,
            Foto = "icon_nopet.png",
        };
        await Shell.Current.GoToAsync($"{nameof(PetAddOrEditPage)}", true,
            new Dictionary<string, object>
            {
                    {"PetDto", PetDto},
                    {"EditCaption", EditCaption},
                    {"IsEditing", IsEditing },
            });
    }

    [RelayCommand]
    async Task GoBack()
    {
        var petId = Id;
        if (petId > 0)
        {
            var response = await _petService.GetPetVMAsync(petId);

            if (response is not null)
            {
                Pet = response;

                await Shell.Current.GoToAsync($"//{nameof(PetDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                            {"PetVM", Pet },
                    });

            }
        }
    }

    [RelayCommand]
    async Task SaveVaccine()
    {
        try
        {
            if (SelectedVaccine.Id == 0)
            {
                var insertedId = await _petVaccinesService.InsertAsync(SelectedVaccine);
                if (insertedId == -1)
                {
                    await Shell.Current.DisplayAlert("Error while updating",
                        $"Please contact administrator..", "OK");
                    return;
                }

                var vaccineDto = await _petVaccinesService.GetPetVaccinesVMAsync(insertedId);

                ShowToastMessage("Registo criado com sucesso");

                await Shell.Current.GoToAsync($"//{nameof(PetDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                        {"SelectedVaccine", vaccineDto}
                    });

            }
            else // Insert (Id > 0)
            {
                var _vaccineId = SelectedVaccine.Id;
                await _petVaccinesService.UpdateAsync(_vaccineId, SelectedVaccine);

                var vaccineDto = await _petVaccinesService.GetPetVaccinesVMAsync(_vaccineId);

                await Shell.Current.GoToAsync($"//{nameof(PetDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                        {"SelectedVaccine", vaccineDto}
                    });

                ShowToastMessage("Registo atualizado com sucesso");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in SaveVaccine: {ex.Message}");
            ShowToastMessage($"Error while creating Vaccine ({ex.Message})");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task OpenGallery(int petId)
    {
        await Shell.Current.GoToAsync($"PetGalleryPage?PetId={petId}");
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