using DaisyPets.Core.Application.ViewModels;

namespace DaisyPets.Web.Maui.ApiServices
{
    public interface IAccessAPIService
    {
        Task<IEnumerable<PetVM>> GetAllPets();
        string GetPetsUrl();
    }
}