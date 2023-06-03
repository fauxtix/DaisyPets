using DaisyPets.Core.Application.ViewModels;

namespace DaisyPets.UI.ApiServices
{
    public interface IAccessAPIService
    {
        Task<IEnumerable<PetVM>> GetAllPets();
        string GetPetsUrl();
    }
}