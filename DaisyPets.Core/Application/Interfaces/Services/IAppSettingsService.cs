namespace DaisyPets.Core.Application.Interfaces.Services
{
    public interface IAppSettingsService
    {
        Task<string> GetLanguage();
        Task SetLanguage(string cultureName);
    }
}