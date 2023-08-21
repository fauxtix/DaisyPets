using DaisyPets.Core.Application.ViewModels.Scheduler;
using DaisyPets.Core.Domain.Scheduler;

namespace DaisyPets.Core.Application.Interfaces.Repositories.Scheduler
{
    public interface IScheduler
    {
        Task DeleteAsync(int Id);
        Task<AppointmentData> FindByIdAsync(int Id);
        Task<IEnumerable<AppointmentData>> GetAllAsync();
        Task<IEnumerable<AppointmentDataDto>> GetAllVMAsync();
        Task<IEnumerable<AppointmentDataDto>> GetAppointmentVMAsync(int Id);
        Task<int> InsertAsync(AppointmentData appointment);
        Task UpdateAsync(int Id, AppointmentData appointment);
    }
}
