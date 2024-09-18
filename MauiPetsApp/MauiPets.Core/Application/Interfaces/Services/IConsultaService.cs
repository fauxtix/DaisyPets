using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;

namespace MauiPetsApp.Core.Application.Interfaces.Services
{
    public interface IConsultaService
    {
        Task DeleteAsync(int Id);
        Task<ConsultaVeterinarioDto> FindByIdAsync(int Id);
        Task<IEnumerable<ConsultaVeterinarioDto>> GetAllAsync();
        Task<IEnumerable<ConsultaVeterinarioVM>> GetAllConsultaVMAsync();
        Task<IEnumerable<ConsultaVeterinarioVM>> GetConsultaVMAsync(int Id);
        Task<int> InsertAsync(ConsultaVeterinarioDto Consulta);
        string RegistoComErros(ConsultaVeterinarioDto appointment);
        Task UpdateAsync(int Id, ConsultaVeterinarioDto Consulta);

    }
}
