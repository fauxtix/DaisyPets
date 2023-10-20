using AutoMapper;
using MauiPetsApp.Core.Domain.TodoManager;
using MauiPetsApp.Core.Application.Interfaces.Repositories.TodoManager;
using MauiPetsApp.Core.Application.Interfaces.Services.TodoManager;
using MauiPetsApp.Core.Application.TodoManager;
using Microsoft.Extensions.Logging;

namespace MauiPetsApp.Infrastructure.Services.ToDoManager
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _repository;

        private readonly IMapper _mapper;
        private readonly ILogger<ToDoService> _logger;

        public ToDoService(IToDoRepository repository, IMapper mapper, ILogger<ToDoService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;

        }
        public async Task DeleteAsync(int Id)
        {
            await _repository.DeleteAsync(Id);
        }

        public async Task<ToDoDto> FindByIdAsync(int Id)
        {
            var resp = await _repository.FindByIdAsync(Id);
            var output = _mapper.Map<ToDoDto>(resp);
            return output;
        }

        public async Task<IEnumerable<ToDoDto>> GetAllAsync()
        {
            var resp = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<ToDoDto>>(resp);
            return output;
        }

        public async Task<IEnumerable<ToDoDto>> GetAllVMAsync()
        {
            try
            {
                var toDosVM = await _repository.GetAllVMAsync();
                return toDosVM;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex.Message}");
                return Enumerable.Empty<ToDoDto>();
            }
        }

        public async Task<IEnumerable<ToDoDto>> GetCompleted()
        {
            var resp = await _repository.GetCompleted();
            var output = _mapper.Map<IEnumerable<ToDoDto>>(resp);
            return output;
        }

        public async Task<IEnumerable<ToDoDto>> GetPending()
        {
            var resp = await _repository.GetPending();
            var output = _mapper.Map<IEnumerable<ToDoDto>>(resp);
            return output;
        }

        public async Task<ToDoDto> GetToDoVM_ByIdAsync(int Id)
        {
            return await _repository.GetToDoVM_ByIdAsync(Id);
        }

        public async Task<int> InsertAsync(ToDoDto toDoDto)
        {
            var todoIdentity = _mapper.Map<ToDo>(toDoDto);
            var insertedId = await _repository.InsertAsync(todoIdentity);
            return insertedId;
        }

        public async Task UpdateAsync(int Id, ToDoDto toDoDto)
        {
            try
            {
                var todoEntity = await _repository.FindByIdAsync(Id);
                if (todoEntity == null)
                    throw new KeyNotFoundException("Item not found");

                var mappedModel = _mapper.Map(toDoDto, todoEntity);

                await _repository.UpdateAsync(Id, mappedModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Erro no update do contacto");
                throw;
            }
        }
    }
}

