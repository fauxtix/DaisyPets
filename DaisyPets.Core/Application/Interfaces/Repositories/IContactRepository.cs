using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;

namespace DaisyPets.Core.Application.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task<int> InsertAsync(Contacto contact);
        Task UpdateAsync(int Id, Contacto contact);
        Task DeleteAsync(int Id);
        Task<IEnumerable<Contacto>> GetAllAsync();
        Task<Contacto> FindByIdAsync(int Id);
        Task<ContactoVM> GetContactVMAsync(int Id);
        Task<IEnumerable<ContactoVM>> GetAllContactVMAsync();
    }
}
