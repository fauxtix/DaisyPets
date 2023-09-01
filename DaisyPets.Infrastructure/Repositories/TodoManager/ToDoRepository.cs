using DaisyPets.Core.Application.Interfaces.DapperContext;
using DaisyPets.Core.Application.Interfaces.Repositories.TodoManager;
using DaisyPets.Core.Application.TodoManager;
using DaisyPets.Core.Domain.TodoManager;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DaisyPets.Infrastructure.Repositories.TodoManager
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly IDapperContext _context;
        private readonly ILogger<ToDoRepository> _logger;

        public ToDoRepository(IDapperContext context, ILogger<ToDoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> InsertAsync(ToDo toDo)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO Todo (");
            sb.Append("Description, StartDate, EndDate, Completed, CategoryId, Generated) ");
            sb.Append(" VALUES(");
            sb.Append("@Description, @StartDate, @EndDate, @Completed, @CategoryId, 0");
            sb.Append(");");
            sb.Append("SELECT last_insert_rowid()");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstAsync<int>(sb.ToString(), param: toDo);
                    return result;
                }

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
                return -1;
            }

        }

        public async Task UpdateAsync(int Id, ToDo toDo)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", toDo.Id);
            dynamicParameters.Add("@Description", toDo.Description);
            dynamicParameters.Add("@StartDate", toDo.StartDate);
            dynamicParameters.Add("@EndDate", toDo.EndDate);
            dynamicParameters.Add("@Completed", toDo.Completed);
            dynamicParameters.Add("@CategoryId", toDo.CategoryId);

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE ToDo SET ");
            sb.Append("Description = @Description, ");
            sb.Append("StartDate = @StartDate, ");
            sb.Append("EndDate = @EndDate, ");
            sb.Append("Completed = @Completed, ");
            sb.Append("CategoryId = @CategoryId ");
            sb.Append("WHERE Id = @Id");

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sb.ToString(), param: dynamicParameters);
            }
        }

        public async Task DeleteAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM ToDo ");
            sb.Append("WHERE Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(sb.ToString(), new { Id });
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
            }
        }

        public async Task<ToDo> FindByIdAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ToDo ");
            sb.Append($"WHERE Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var pet = await connection.QuerySingleOrDefaultAsync<ToDo>(sb.ToString(), new { Id });
                    if (pet != null)
                    {
                        return pet;
                    }
                    else
                    {
                        return new ToDo();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex);
                throw;
            }
        }

        public async Task<IEnumerable<ToDo>> GetAllAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ToDo ");
            sb.Append("ORDER BY date(StartDate) DESC");
            using (var connection = _context.CreateConnection())
            {
                var toDos = await connection.QueryAsync<ToDo>(sb.ToString());
                if (toDos != null)
                {
                    return toDos;
                }
                else
                {
                    return Enumerable.Empty<ToDo>();
                }
            }
        }

        public async Task<IEnumerable<ToDoDto>> GetAllVMAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ToDo.Id, ToDo.Description, StartDate, EndDate, Completed, Generated, ");
            sb.Append("TodoCategories.Id as [CategoryId], TodoCategories.Descricao AS [CategoryDescription] ");
            sb.Append("FROM ToDo ");
            sb.Append("INNER JOIN ToDoCategories ON ");
            sb.Append("ToDo.CategoryId = ToDoCategories.Id ");
            sb.Append("ORDER BY date(StartDate) DESC");

            using (var connection = _context.CreateConnection())
            {
                var ToDosVM = await connection.QueryAsync<ToDoDto>(sb.ToString());
                if (ToDosVM != null)
                {
                    return ToDosVM;
                }
                else
                {
                    return Enumerable.Empty<ToDoDto>();
                }
            }
        }

        public async Task<IEnumerable<ToDoDto>> GetToDoVM_ByIdAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ToDo.Id, ToDo.Description, StartDate, EndDate, Completed, Generated, ");
            sb.Append("TodoCategories.Id, TodoCategories.Descricao AS [CategoryDescription] ");
            sb.Append("FROM ToDo ");
            sb.Append("INNER JOIN ToDoCategories ON ");
            sb.Append("ToDo.CategoryId = ToDoCategories.Id ");
            sb.Append("WHERE ToDo.Id = @Id");


            using (var connection = _context.CreateConnection())
            {
                var ToDoVM = await connection.QueryAsync<ToDoDto>(sb.ToString(), new { Id });
                if (ToDoVM != null)
                {
                    return ToDoVM;
                }
                else
                {
                    return Enumerable.Empty<ToDoDto>();
                }
            }
        }

        public async Task<IEnumerable<ToDoDto>> GetPending()
        {
            var query = GetSelectByCompletedString(1);

            using (var connection = _context.CreateConnection())
            {
                var pendingToDos = await connection.QueryAsync<ToDoDto>(query);
                if (pendingToDos != null)
                {
                    return pendingToDos;
                }
                else
                {
                    return Enumerable.Empty<ToDoDto>();
                }
            }
        }

        public async Task<IEnumerable<ToDoDto>> GetCompleted()
        {
            var query = GetSelectByCompletedString(2);

            using (var connection = _context.CreateConnection())
            {
                var completedToDos = await connection.QueryAsync<ToDoDto>(query);
                if (completedToDos != null)
                {
                    return completedToDos;
                }
                else
                {
                    return Enumerable.Empty<ToDoDto>();
                }
            }
        }

        private string GetSelectByCompletedString(int Completed)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  ToDo.Id, ToDo.Description, StartDate, EndDate, ");
            sb.Append("Completed, Generated, TodoCategories.Descricao AS [CategoryDescription] ");
            sb.Append("FROM ToDo ");
            sb.Append("INNER JOIN ToDoCategories ON ");
            sb.Append("ToDo.CategoryId = ToDoCategories.Id ");
            sb.Append("WHERE ToDo.Id = @Id AND ");
            sb.Append($"Todo.Completed = {Completed}");

            return sb.ToString();

        }
    }
}
