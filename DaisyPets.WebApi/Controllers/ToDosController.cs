using DaisyPets.Core.Application.Interfaces.Services.TodoManager;
using DaisyPets.Core.Application.TodoManager;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.WebApi.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DaisyPets.WebApi.Controllers
{
    /// <summary>
    /// Todo controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]


    public class ToDosController : ControllerBase
    {
        private readonly IToDoService _service;
        private readonly ILogger<ToDosController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public ToDosController(IToDoService service, ILogger<ToDosController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ToDoDto todoDto)
        {
            var location = GetControllerActionNames();
            try
            {

                if (todoDto is null)
                {
                    return BadRequest();
                }

                var validator = new ToDoValidator();
                var result = validator.Validate(todoDto);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                var insertedId = await _service.InsertAsync(todoDto);
                var viewTodo = await _service.FindByIdAsync(insertedId);
                var actionReturned = CreatedAtAction(nameof(Get), new { id = viewTodo.Id }, viewTodo);


                return Ok(new { Id = insertedId });

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Get item by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var appt = await _service.FindByIdAsync(Id);
                if (appt is null)
                {
                    return NotFound();
                }

                return Ok(appt);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Update(int Id, [FromBody] ToDoDto todoDto)
        {
            var location = GetControllerActionNames();

            try
            {

                if (todoDto == null)
                {
                    string msg = "O registo passado como paràmetro é incorreto.";
                    _logger.LogWarning(msg);
                    return BadRequest(msg);
                }

                if (Id != todoDto.Id)
                {
                    return BadRequest($"O id ({Id}) passado como paràmetro é incorreto");
                }

                var viewAppt = _service.GetToDoVM_ByIdAsync(Id);
                if (viewAppt == null)
                {
                    return NotFound("Consulta não foi encontrada");
                }

                var validator = new ToDoValidator();
                var result = validator.Validate(todoDto);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                await _service.UpdateAsync(Id, todoDto);
                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, "Erro ao atualizar consulta (API)");
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var viewAppt = _service.GetToDoVM_ByIdAsync(Id);
                if (viewAppt == null)
                {
                    return NotFound("Item não foi encontrado");
                }

                await _service.DeleteAsync(Id);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        [HttpGet("AllConsultaVM")]
        public async Task<IActionResult> AllConsultaVM()
        {
            try
            {
                var listOfTodos = await _service.GetAllVMAsync();
                if (listOfTodos is null)
                { return NotFound(); }

                return Ok(listOfTodos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Não foram retornados quaisquer dados");
            }
        }

        [HttpGet("PendingTodos")]
        public async Task<IActionResult> GetPendingTodos()
        {
            try
            {
                var listOfPendingTodos = await _service.GetPending();
                if (listOfPendingTodos is null)
                { return NotFound(); }

                return Ok(listOfPendingTodos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Não foram retornados quaisquer dados");
            }
        }

        [HttpGet("CompletedTodos")]
        public async Task<IActionResult> GeCompletedTodos()
        {
            try
            {
                var listOCompletedTodos = await _service.GetCompleted();
                if (listOCompletedTodos is null)
                { return NotFound(); }

                return Ok(listOCompletedTodos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Não foram retornados quaisquer dados");
            }
        }


        [HttpPost("ValidateToDo")]
        public IActionResult ValidateToDo([FromBody] ToDoDto todoDto)
        {
            // Create validator instance (or inject it)
            var ToDoValidator = new ToDoValidator();

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = ToDoValidator.Validate(todoDto);

            if (result.IsValid)
            {
                return Ok(todoDto);
            }

            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessages);
        }

        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;

            return $"{controller} - {action}";
        }

        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, $"Algo de errado ocorreu ({message}). Contacte o Administrador");
        }


    }
}
