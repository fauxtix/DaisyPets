﻿using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Core.Domain;
using DaisyPets.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using DaisyPets.Application.Interfaces.Services;

namespace DaisyPets.WebApi.Controllers
{
    /// <summary>
    /// Gestor de despesas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly IDespesaService _service;
        private readonly ILogger<DespesaController> _logger;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public DespesaController(IDespesaService service, ILogger<DespesaController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Cria nova despesa
        /// </summary>
        /// <param name="expense"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(DespesaDto expense)
        {
            var location = GetControllerActionNames();
            try
            {

                if (expense is null)
                {
                    return BadRequest();
                }

                var validator = new DespesaValidator();
                var result = validator.Validate(expense);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                var insertedId = await _service.InsertAsync(expense);
                var viewAppt = await _service.GetByIdAsync(insertedId);
                var actionReturned = CreatedAtAction(nameof(Get), new { id = viewAppt.Id }, viewAppt);


                return Ok(new { Id = insertedId });

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Atualiza despesa
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="expense"></param>
        /// <returns></returns>
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Update(int Id, [FromBody] DespesaDto expense)
        {
            var location = GetControllerActionNames();

            try
            {

                if (expense == null)
                {
                    string msg = "A despesa passada como paràmetro é incorreto.";
                    _logger.LogWarning(msg);
                    return BadRequest(msg);
                }

                if (Id != expense.Id)
                {
                    return BadRequest($"O id ({Id}) passado como paràmetro é incorreto");
                }

                var viewExpense = await _service.GetByIdAsync(Id);
                if (viewExpense == null)
                {
                    return NotFound("Despesa não foi encontrada");
                }

                var validator = new DespesaValidator();
                var result = validator.Validate(expense);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                await _service.UpdateAsync(Id, expense);
                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, "Erro ao atualizar consulta (API)");
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        ///  Apaga despesa
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var viewExpense = await _service.GetByIdAsync(Id);
                if (viewExpense == null)
                {
                    return NotFound("Despesa não foi encontrada");
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


        /// <summary>
        /// Pesquisa despesa por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var expense = await _service.GetByIdAsync(Id);
                if (expense is null)
                {
                    return NotFound();
                }

                return Ok(expense);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Get VM by Id (grid)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("VMExpenseByIdAsync/{Id:int}")]
        public async Task<IActionResult> GetVMExpenseByIdAsync(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var expenseVM = await _service.GetVMByIdAsync(Id);
                if (expenseVM is null)
                {
                    return NotFound($"{Id} não encontrado");
                }

                return Ok(expenseVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }


        /// <summary>
        /// Todas as despesas
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var location = GetControllerActionNames();

            try
            {
                var expenses = await _service.GetAllAsync();
                if (expenses is null)
                {
                    return NotFound();
                }

                return Ok(expenses);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Todas as despesas VM (preenchimento de grid)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("AllVMAsync")]
        public async Task<IActionResult> GetAllVMAsync()
        {
            var location = GetControllerActionNames();

            try
            {
                var expensesVM = await _service.GetAllVMAsync();
                if (expensesVM is null)
                {
                    return NotFound();
                }

                return Ok(expensesVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }


        /// <summary>
        /// Despesas por categoria
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("TipoDespesa_ByCategoriaDespesa/{Id:int}")]
        public async Task<IActionResult> GetTipoDespesa_ByCategoriaDespesa(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var categoryType = await _service.GetTipoDespesa_ByCategoriaDespesa(Id);
                return categoryType!.Any() == false ? NotFound() : Ok(categoryType);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Tipo de despesas
        /// </summary>
        /// <returns></returns>
        [HttpGet("TipoDespesas")]
        public async Task<IActionResult> GetTipoDespesas()
        {
            var location = GetControllerActionNames();

            try
            {
                var categoryType = await _service.GetTipoDespesas();
                return categoryType!.Any() == false ? NotFound() : Ok(categoryType);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Descrição de categoria de despesas
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("DescricaoCategoriaDespesa/{Id:int}")]
        public async Task<IActionResult> GetDescricaoCategoriaDespesa(int Id)
        {
            var location = GetControllerActionNames();
            var categoriaDespesa = await _service.GetDescricaoCategoriaDespesa(Id);
            if(string.IsNullOrEmpty(categoriaDespesa.Descricao)) return NotFound();

            return Ok(categoriaDespesa);
        }

        /// <summary>
        /// Valida despesa
        /// </summary>
        /// <param name="_expense"></param>
        /// <returns></returns>
        /// 
        [HttpPost("ValidateExpense")]
        public IActionResult ValidateExpense([FromBody] DespesaDto _expense)
        {
            var expenseValidator = new DespesaValidator();

            var result = expenseValidator.Validate(_expense);

            if (result.IsValid)
            {
                return Ok(_expense);
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
