using MauiPets.Core.Domain;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;

namespace MauiPetsApp.Core.Application.Interfaces.Repositories
{
    public interface IVacinasRepository
    {

        Task<int> InsertAsync(Vacina contact);
        Task UpdateAsync(int Id, Vacina contact);
        Task DeleteAsync(int Id);
        Task<IEnumerable<Vacina>> GetAllAsync();
        Task<Vacina> FindByIdAsync(int Id);
        Task<VacinaVM> GetVacinaVMAsync(int Id);
        Task<IEnumerable<VacinaVM>> GetAllVacinasVMAsync();
        Task<IEnumerable<VacinaVM>> GetPetVaccinesVMAsync(int petId);
        Task<IEnumerable<TipoVacina>> GetTipoVacinasAsync(int specie);
    }
}
