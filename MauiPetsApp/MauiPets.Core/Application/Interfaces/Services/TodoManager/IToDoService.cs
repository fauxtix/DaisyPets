using MauiPetsApp.Core.Application.TodoManager;

namespace MauiPetsApp.Core.Application.Interfaces.Services.TodoManager
{
    public interface IToDoService
    {
        Task<int> InsertAsync(ToDoDto toDo);
        Task UpdateAsync(int Id, ToDoDto toDo);
        Task DeleteAsync(int Id);
        Task<ToDoDto> FindByIdAsync(int Id);
        Task<IEnumerable<ToDoDto>> GetAllAsync();
        Task<IEnumerable<ToDoDto>> GetAllVMAsync();
        Task<IEnumerable<ToDoDto>> GetCompleted();
        Task<IEnumerable<ToDoDto>> GetPending();
        Task<ToDoDto> GetToDoVM_ByIdAsync(int Id);
    }
}
