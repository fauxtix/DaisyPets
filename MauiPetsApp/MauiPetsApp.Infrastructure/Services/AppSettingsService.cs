using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.Interfaces.Services;

namespace MauiPetsApp.Infrastructure.Services
{
    public class AppSettingsService : IAppSettingsService
    {
        private readonly IAppSettingsRepository _repository;
        public AppSettingsService(IAppSettingsRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> GetLanguage()
        {
            return await _repository.GetLanguage();
        }

        public async Task SetLanguage(string cultureName)
        {
            await _repository.SetLanguage(cultureName);
        }
    }
}
