using DaisyPets.Core.Application.Interfaces.Services.Sceduler;
using DaisyPets.Core.Application.ViewModels.Scheduler;
using DaisyPets.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;

namespace DaisyPets.WebApi.Controllers
{
    /// <summary>
    /// Appointments controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]


    public class AppointmentController : ControllerBase
    {
        private readonly ISchedulerService _apptService;
        private readonly ILogger<AppointmentController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apptService"></param>
        /// <param name="logger"></param>
        public AppointmentController(ISchedulerService apptService, ILogger<AppointmentController> logger)
        {
            _apptService = apptService;
            _logger = logger;
        }

        /// <summary>
        /// Create new appointment
        /// </summary>
        /// <param name="appt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(AppointmentDataDto appt)
        {
            var location = GetControllerActionNames();
            try
            {

                if (appt is null)
                {
                    return BadRequest();
                }

                var validator = new Appointmentvalidator();
                var result = validator.Validate(appt);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                var insertedId = await _apptService.InsertAsync(appt);
                var viewAppt = await _apptService.FindByIdAsync(insertedId);
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
        /// Update appointment
        /// </summary>
        /// <param name="Id">Appt Id</param>
        /// <param name="appt">Appt data</param>
        /// <returns></returns>
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Update(int Id, [FromBody] AppointmentDataDto appt)
        {
            var location = GetControllerActionNames();

            try
            {

                if (appt == null)
                {
                    string msg = "A marcação passada como paràmetro é incorreto.";
                    _logger.LogWarning(msg);
                    return BadRequest(msg);
                }

                if (Id != appt.Id)
                {
                    return BadRequest($"O id ({Id}) passado como paràmetro é incorreto");
                }

                var viewAppt = await _apptService.FindByIdAsync(Id);
                if (viewAppt == null)
                {
                    return NotFound("Marcação não foi encontrada");
                }

                var validator = new Appointmentvalidator();
                var result = validator.Validate(appt);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                await _apptService.UpdateAsync(Id, appt);
                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, "Erro ao atualizar marcação (API)");
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
                var viewAppt = await _apptService.FindByIdAsync(Id);
                if (viewAppt == null)
                {
                    return NotFound("Marcação não foi encontrada");
                }

                await _apptService.DeleteAsync(Id);
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
                var appt = await _apptService.FindByIdAsync(Id);
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
        /// All appointments
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllAppointmentsVM")]
        public async Task<IActionResult> GetAllAppointmentsVM()
        {
            try
            {
                var listOfAppointments = await _apptService.GetAllAsync();
                if (listOfAppointments.Any())
                    return Ok(listOfAppointments);

                { return NotFound(); }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados das marcações não encontrados");
            }
        }

        /// <summary>
        /// Validate Appointment
        /// </summary>
        /// <param name="appt"></param>
        /// <returns></returns>
        [HttpPost("ValidateAppointment")]
        public IActionResult ValidateAppointment([FromBody] AppointmentDataDto appt)
        {
            // Create validator instance (or inject it)
            var apptValidator = new Appointmentvalidator();

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = apptValidator.Validate(appt);

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
