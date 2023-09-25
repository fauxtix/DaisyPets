using MauiPetsApp.Core.Application.ViewModels.Scheduler;

namespace MauiPetsApp.Core.Application.Interfaces.Services.Sceduler
{
    public interface ISchedulerService
    {
        Task DeleteAsync(int Id);
        Task<AppointmentDataDto> FindByIdAsync(int Id);
        Task<IEnumerable<AppointmentDataDto>> GetAllAsync();
        Task<IEnumerable<AppointmentDataDto>> GetAllVMAsync();
        Task<IEnumerable<AppointmentDataDto>> GetAppointmentVMAsync(int Id);
        Task<int> InsertAsync(AppointmentDataDto appointment);
        Task UpdateAsync(int Id, AppointmentDataDto appointment);

    }
}
