using MauiPetsApp.Core.Application.TodoManager;
using MauiPetsApp.Core.Domain.TodoManager;

namespace MauiPetsApp.Core.Application.Interfaces.Repositories.TodoManager
{
    public interface IToDoRepository
    {
        Task DeleteAsync(int Id);
        Task<ToDo> FindByIdAsync(int Id);
        Task<IEnumerable<ToDo>> GetAllAsync();
        Task<IEnumerable<ToDoDto>> GetAllVMAsync();
        Task<IEnumerable<ToDoDto>> GetCompleted();
        Task<IEnumerable<ToDoDto>> GetPending();
        Task<ToDoDto> GetToDoVM_ByIdAsync(int Id);
        Task<int> InsertAsync(ToDo toDo);
        Task<IEnumerable<ToDoDto>> SearchTodosByTextAsync(string filter);
        Task UpdateAsync(int Id, ToDo toDo);
    }
}