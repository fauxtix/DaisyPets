using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;

namespace MauiPetsApp.Core.Application.Interfaces.Services
{
    public interface IPetService
    {
        Task<int> InsertAsync(PetDto pet);
        Task UpdateAsync(int Id, PetDto pet);
        Task<bool> DeleteAsync(int Id);
        Task<IEnumerable<PetDto>> GetAllAsync();
        Task<PetDto> FindByIdAsync(int Id);
        Task<PetVM> GetPetVMAsync(int Id);
        Task<IEnumerable<PetVM>> GetAllVMAsync();
        Task<IEnumerable<PesoDto>> GetPesos();
        Task<string> GetDescriptionBySizeAndMonths(int IdTamanho, int meses);
        string RegistoComErros(PetDto pet);
    }
}
