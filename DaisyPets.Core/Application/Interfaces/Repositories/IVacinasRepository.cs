using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaisyPets.Core.Application.Interfaces.Repositories
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
    }
}
