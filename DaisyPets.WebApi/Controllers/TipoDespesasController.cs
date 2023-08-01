using AutoMapper;
using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;

namespace DaisyPets.WebApi.Controllers
{
    /// <summary>
    /// Controller de tipo de despesas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDespesasController : ControllerBase
    {
        private readonly ILogger<TipoDespesasController> _logger;
        private readonly ITipoDespesaService _service;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        /// <param name="mapper"></param>

        public TipoDespesasController(ILogger<TipoDespesasController> logger,
                                      ITipoDespesaService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Cria tipo de despesa
        /// </summary>
        /// <param name="tipoDespesa"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriaTipoDespesa([FromBody] TipoDespesaDto tipoDespesa)
        {
            var location = GetControllerActionNames();
            try
            {
                if (tipoDespesa == null)
                {
                    return BadRequest();
                }

                var validator = new TipoDespesaValidator();
                var result = validator.Validate(tipoDespesa);

                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                var insertedId = await _service.Insert(tipoDespesa);
                var createdExpenseType = await _service.Get_ById(insertedId);
                var actionReturned = CreatedAtAction(nameof(GetTipoDespesaById), new { Id = insertedId }, createdExpenseType);
                return Ok(new { Id = insertedId });
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Atualiza tipo de despesa
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="expenseType"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizaTipoDespesa(int Id, [FromBody] TipoDespesaDto expenseType)
        {
            var location = GetControllerActionNames();
            try
            {
                if (expenseType == null || Id != expenseType.Id)
                    return BadRequest();
                if (await _service.Get_ById(Id) == null)
                {
                    return NotFound();
                }

                var validator = new TipoDespesaValidator();
                var result = validator.Validate(expenseType);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                await _service.Update(Id, expenseType);
                return NoContent();
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Apaga tipo de despesa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ApagaTipoDespesa(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                if (id < 1)
                {
                    _logger.LogWarning($"{location}: Delete failed with bad data - id: {id}");
                    return BadRequest();
                }
                var despesaAApagar = await _service.Get_ById(id);
                if (despesaAApagar == null)
                {
                    return NotFound($"Despesa com o Id {id} não encontrado");
                }

                var okToDelete = await _service.CanRecordBeDeleted(id);
                if (okToDelete)
                {
                    await _service.Delete(id);
                    return NoContent();
                }
                else
                    return BadRequest("Tipo de despesa com pagamentos. Pedido cancelado");
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }


        /// <summary>
        /// Todos os registos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AllTipoDespesas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAllTipoDespesas()
        {
            var location = GetControllerActionNames();
            try
            {
                var result = await _service.GetAll();
                if (result.Any())
                {
                    return Ok(result);
                }

                return Ok(Enumerable.Empty<TipoDespesaVM>());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro no Api (GetTipoDespesas): {e.Message}");
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        
        /// <summary>
        /// Todos os registos apresentados na grid
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AllTipoDespesasVM")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      
        public async Task<IActionResult> GetAllTipoDespesasVM()
        {
            var location = GetControllerActionNames();
            try
            {
                var result = await _service.GetAllVM();
                if (result.Any())
                {
                    return Ok(result);
                }

                return Ok(Enumerable.Empty<TipoDespesaVM>());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro no Api (GetTipoDespesas): {e.Message}");
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }


        /// <summary>
        /// Tipo de despesa por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TipoDespesaById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetTipoDespesaById(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }
                var result = await _service.Get_ById(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro no Api (GetExpenseTypeById): {e.Message}");
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }


        /// <summary>
        /// get registo (VM)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TipoDespesaByIdVM/{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetTipoDespesaVMById(int Id)
        {
            var location = GetControllerActionNames();
            try
            {
                if (Id < 1)
                {
                    return BadRequest();
                }
                var result = await _service.GetTipoDespesaVM_ById(Id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro no Api (GetExpenseTypeById): {e.Message}");
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }


        /// <summary>
        /// Valida tipo de despesa
        /// </summary>
        /// <param name="expense"></param>
        /// <returns></returns>
        [HttpPost("ValidateExpenseType")]
        public IActionResult ValidateExpenseType([FromBody] TipoDespesaDto expense)
        {
            // Create validator instance (or inject it)
            var expenseTypeValidator = new TipoDespesaValidator();

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = expenseTypeValidator.Validate(expense);

            if (result.IsValid)
            {
                return Ok(expense);
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
            return StatusCode(500, "Algo de errado ocorreu. Contacte o Administrador");
        }
    }
}
