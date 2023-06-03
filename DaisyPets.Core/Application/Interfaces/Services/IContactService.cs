using DaisyPets.Core.Application.ViewModels;

namespace DaisyPets.Core.Application.Interfaces.Services
{
    public interface IContactService
    {
        Task<int> InsertAsync(ContactoVM contact);
        Task UpdateAsync(int Id, ContactoVM contact);
        Task DeleteAsync(int Id);
        Task<IEnumerable<ContactoVM>> GetAllAsync();
        Task<ContactoVM> FindByIdAsync(int Id);
        Task<ContactoVM> GetContactVMAsync(int Id);
        Task<IEnumerable<ContactoVM>> GetAllContactVMAsync();
        string RegistoComErros(ContactoVM contacto);


    }
}
