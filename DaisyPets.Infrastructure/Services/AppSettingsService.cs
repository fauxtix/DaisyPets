using DaisyPets.Core.Application.Interfaces.Repositories;
using DaisyPets.Core.Application.Interfaces.Services;

namespace DaisyPets.Infrastructure.Services
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
