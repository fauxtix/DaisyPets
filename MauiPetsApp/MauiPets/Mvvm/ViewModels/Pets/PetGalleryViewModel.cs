using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Controls;
using MauiPets.Core.Application.Interfaces.Services;
using MauiPets.Core.Application.ViewModels;
using MauiPets.Mvvm.Views.Pets;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Pets;

[QueryProperty(nameof(PetId), "PetId")]
public partial class PetGalleryViewModel : ObservableObject, IQueryAttributable
{
    private readonly IPetPhotoService _photoService;

    [ObservableProperty]
    ObservableCollection<PetPhotoDto> photos = new();

    [ObservableProperty]
    PetPhotoDto selectedPhoto;

    [ObservableProperty]
    int petId;

    [ObservableProperty]
    bool isBusy;

    public PetGalleryViewModel(IPetPhotoService photoService)
    {
        _photoService = photoService;
    }

    public async Task LoadPhotosAsync()
    {
        if (PetId <= 0) return;
        IsBusy = true;
        Photos.Clear();
        var list = await _photoService.GetPhotosAsync(PetId);
        foreach (var photo in list)
            Photos.Add(photo);
        IsBusy = false;
    }



    [RelayCommand]
    public async Task AddPhotoAsync()
    {
        if (PetId <= 0) return;

        var result = await MediaPicker.PickPhotoAsync();
        if (result != null)
        {
            var localPath = Path.Combine(FileSystem.Current.AppDataDirectory, $"{Guid.NewGuid()}.jpg");
            using var stream = await result.OpenReadAsync();
            using var file = File.Create(localPath);
            await stream.CopyToAsync(file);

            await _photoService.AddPhotoAsync(PetId, localPath);
            Photos.Insert(0, new PetPhotoDto { PetId = PetId, PhotoPath = localPath, DateAdded = DateTime.Now });
        }
    }

    [RelayCommand]
    public void ShowPhoto(PetPhotoDto photo)
    {
        SelectedPhoto = photo;
        var popup = new PhotoPopup(photo.PhotoPath);
        Application.Current.MainPage.ShowPopup(popup);
    }

    [RelayCommand]
    public async Task DeletePhotoAsync(PetPhotoDto photo)
    {
        bool confirm = await Application.Current.MainPage.DisplayAlert(
            "Confirmação",
            "Deseja realmente apagar esta foto?",
            "Sim",
            "Não"
        );
        if (!confirm)
            return;

        await _photoService.DeletePhotoAsync(photo.Id);
        Photos.Remove(photo);
    }

    [RelayCommand]
    public void ClearPhotos()
    {
        Photos.Clear();
    }

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync($"//{nameof(PetsPage)}");
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("PetId", out var petIdObj) && int.TryParse(petIdObj?.ToString(), out int petId))
        {
            PetId = petId;
            await LoadPhotosAsync();
        }
    }
}