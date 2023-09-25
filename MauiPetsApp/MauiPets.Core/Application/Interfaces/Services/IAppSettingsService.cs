namespace MauiPetsApp.Core.Application.Interfaces.Services
{
    public interface IAppSettingsService
    {
        Task<string> GetLanguage();
        Task SetLanguage(string cultureName);
    }
}