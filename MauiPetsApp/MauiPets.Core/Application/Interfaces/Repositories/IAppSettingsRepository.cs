namespace MauiPetsApp.Core.Application.Interfaces.Repositories
{
    public interface IAppSettingsRepository
    {
        Task<string> GetLanguage();
        Task SetLanguage(string cultureName);
    }
}