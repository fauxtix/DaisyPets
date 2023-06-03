using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;

namespace DaisyPets.Core.Application.Interfaces.Services
{
    public interface IPetService
    {
        Task<int> InsertAsync(PetDto pet);
        Task UpdateAsync(int Id, PetDto pet);
        Task DeleteAsync(int Id);
        Task<IEnumerable<PetDto>> GetAllAsync();
        Task<PetDto> FindByIdAsync(int Id);
        Task<PetVM> GetPetVMAsync(int Id);
        Task<IEnumerable<PetVM>> GetAllVMAsync();
        Task<IEnumerable<PesoDto>> GetPesos();
        Task<string> GetDescriptionBySizeAndMonths(int IdTamanho, int meses);
    }
}
