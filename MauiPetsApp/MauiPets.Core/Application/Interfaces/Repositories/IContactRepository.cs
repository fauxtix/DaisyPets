using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;

namespace MauiPetsApp.Core.Application.Interfaces.Repositories
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
        Task<IEnumerable<ContactoVM>> SearchContactByNamet(string filter);
    }
}
