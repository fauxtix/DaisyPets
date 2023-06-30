using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;

namespace DaisyPets.Core.Application.Interfaces.Application
{
    public interface IPetRepository
    {
        Task<int> InsertAsync(Pet pet);
        Task UpdateAsync(int Id, Pet pet);
        Task DeleteAsync(int Id);
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<Pet> FindByIdAsync(int Id);
        Task<PetVM> GetPetVMAsync(int Id);
        Task<IEnumerable<PetVM>> GetAllVMAsync();
        Task<IEnumerable<Peso>> GetPesos();
        Task<string> GetDescriptionBySizeAndMonths(int IdTamanho, int meses);
        Task<bool> CanPetBeDeleted(int Id);
    }
}
