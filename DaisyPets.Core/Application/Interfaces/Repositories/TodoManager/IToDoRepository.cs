using DaisyPets.Core.Application.TodoManager;
using DaisyPets.Core.Domain.TodoManager;

namespace DaisyPets.Core.Application.Interfaces.Repositories.TodoManager
{
    public interface IToDoRepository
    {
        Task DeleteAsync(int Id);
        Task<ToDo> FindByIdAsync(int Id);
        Task<IEnumerable<ToDo>> GetAllAsync();
        Task<IEnumerable<ToDoDto>> GetAllVMAsync();
        Task<IEnumerable<ToDoDto>> GetCompleted();
        Task<IEnumerable<ToDoDto>> GetPending();
        Task<IEnumerable<ToDoDto>> GetToDoVM_ByIdAsync(int Id);
        Task<int> InsertAsync(ToDo toDo);
        Task UpdateAsync(int Id, ToDo toDo);
    }
}