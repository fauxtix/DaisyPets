using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;
using DaisyPets.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace DaisyPets.WebApi.Controllers
{
    /// <summary>
    /// Consulta controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]


    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaService;
        private readonly ILogger<ConsultaController> _logger;

        /// <summary>
        /// Consulta controller constructor
        /// </summary>
        /// <param name="consultaService"></param>
        public ConsultaController(IConsultaService consultaService, ILogger<ConsultaController> logger)
        {
            _consultaService = consultaService;
            _logger = logger;
        }

        /// <summary>
        /// Create new appointment
        /// </summary>
        /// <param name="appt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(ConsultaVeterinarioDto appt)
        {
            var location = GetControllerActionNames();
            try
            {

                if (appt is null)
                {
                    return BadRequest();
                }

                var validator = new ConsultaValidator();
                var result = validator.Validate(appt);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                var insertedId = await _consultaService.InsertAsync(appt);
                var viewAppt = await _consultaService.FindByIdAsync(insertedId);
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
        /// Atualiza consulta
        /// </summary>
        /// <param name="Id">Id da consulta</param>
        /// <param name="appt">Dados da consulta</param>
        /// <returns></returns>
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Update(int Id, [FromBody] ConsultaVeterinarioDto appt)
        {
            var location = GetControllerActionNames();

            try
            {

                if (appt == null)
                {
                    string msg = "A Consulta passada como paràmetro é incorreto.";
                    _logger.LogWarning(msg);
                    return BadRequest(msg);
                }

                if (Id != appt.Id)
                {
                    return BadRequest($"O id ({Id}) passado como paràmetro é incorreto");
                }

                var viewAppt = await _consultaService.GetConsultaVMAsync(Id);
                if (viewAppt == null)
                {
                    return NotFound("Consulta não foi encontrada");
                }

                var validator = new ConsultaValidator();
                var result = validator.Validate(appt);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                await _consultaService.UpdateAsync(Id, appt);
                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, "Erro ao atualizar consulta (API)");
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Delete Appointment
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var viewAppt = await _consultaService.GetConsultaVMAsync(Id);
                if (viewAppt == null)
                {
                    return NotFound("Consulta não foi encontrada");
                }

                await _consultaService.DeleteAsync(Id);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }


        /// <summary>
        /// Get appt by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var appt = await _consultaService.FindByIdAsync(Id);
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

        /// <summary>
        /// Devolve todas consultas
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllConsultaVM")]
        public async Task<IActionResult> AllConsultaVM()
        {
            try
            {
                var listOfConsultas = await _consultaService.GetAllConsultaVMAsync();
                if (listOfConsultas is null)
                { return NotFound(); }

                return Ok(listOfConsultas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados das consultas não encontrados");
            }
        }

        /// <summary>
        /// Devolve consulta por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("ApptVMById/{Id:int}")]
        public async Task<IActionResult> AppointmentVMById(int Id)
        {
            try
            {
                var apptVM = await _consultaService.GetConsultaVMAsync(Id);

                if (apptVM is null)
                { return NotFound(); }

                return Ok(apptVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados das consultas não encontrados");
            }
        }

        /// <summary>
        /// Pet appointments
        /// </summary>
        /// <param name="Id">Pet Id</param>
        /// <returns>List of appointments</returns>
        [HttpGet("PetAppointments/{Id:int}")]
        public async Task<IActionResult> PetAppointments(int Id)
        {
            try
            {
                var petAppts = await _consultaService.GetConsultaVMAsync(Id);

                if (petAppts is null)
                {
                    return NotFound();
                }

                return Ok(petAppts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados das consultas não encontrados");
            }
        }


        /// <summary>
        /// Validate Appointment
        /// </summary>
        /// <param name="appt"></param>
        /// <returns></returns>
        [HttpPost("ValidateAppointment")]
        public IActionResult ValidateConsulta([FromBody] ConsultaVeterinarioDto appt)
        {
            // Create validator instance (or inject it)
            var ConsultaValidator = new ConsultaValidator();

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = ConsultaValidator.Validate(appt);

            if (result.IsValid)
            {
                return Ok(appt);
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
