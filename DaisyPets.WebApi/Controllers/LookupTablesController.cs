using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using Microsoft.AspNetCore.Mvc;

namespace DaisyPets.WebApi.Controllers
{
    /// <summary>
    /// Controller de Lookup Tables
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LookupTablesController : ControllerBase
    {
        private readonly ILookupTableService _service;
        private readonly ILogger<LookupTablesController> _logger;
        /// <summary>
        /// Lookup tables constructor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public LookupTablesController(ILookupTableService service,
            ILogger<LookupTablesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Get all data from a lookup table
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpGet("GetAllRecords/{tableName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAllRecords(string tableName)
        {
            var location = GetControllerActionNames();
            try
            {
                var data = await _service.GetLookupTableData(tableName);
                if (data.Any())
                {
                    return Ok(data);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Get lookup description by id and table name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpGet("GetDescriptionByIdAndTable/{id:int}/{tableName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetDescriptionByIdAndTable(int id, string tableName)
        {
            var location = GetControllerActionNames();
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }
                if (string.IsNullOrEmpty(tableName))
                {
                    return BadRequest();
                }
                var description = await _service.GetDescription(id, tableName);
                if (!string.IsNullOrEmpty(description))
                {
                    return Ok(description);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Get lookup table PK
        /// </summary>
        /// <param name="description"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpGet("GetPKByDescriptionAndTable/{description}/{tableName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetPKByDescriptionAndTable(string description, string tableName)
        {
            var location = GetControllerActionNames();
            try
            {
                if (string.IsNullOrEmpty(description))
                {
                    return BadRequest();
                }
                if (string.IsNullOrEmpty(tableName))
                {
                    return BadRequest();
                }
                var tablePK = await _service.GetId(description, tableName);
                if (tablePK > 0)
                {
                    return Ok(tablePK);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }


        /// <summary>
        /// Get table record by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpGet("{id:int}/{tableName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetRecordById(int id, string tableName)
        {
            var location = GetControllerActionNames();
            try
            {
                if (id < 1)
                {
                    return BadRequest($"O id ({id}) passado como paràmetro é incorreto");
                }
                if (string.IsNullOrEmpty(tableName))
                {
                    return BadRequest($"O nome da tabela ({tableName}) é incorreto");
                }

                var tableRecord = await _service.GetRecordById(id, tableName);
                if (tableRecord is not null)
                {
                    return Ok(tableRecord);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Verify if lookup description already exist in the DB
        /// </summary>
        /// <param name="description"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>

        [HttpGet("CheckRecordExist/{description}/{tableName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CheckRecordExist(string description, string tableName)
        {
            var location = GetControllerActionNames();
            try
            {
                if (string.IsNullOrEmpty(description))
                {
                    return BadRequest($"A descrição ({description}) passada como paràmetro é incorreta");
                }
                if (string.IsNullOrEmpty(tableName))
                {
                    return BadRequest($"O nome da tabela ({tableName}) é incorreta");
                }

                var descriptionExist = await _service.CheckRegistoExist(description, tableName);
                return Ok(descriptionExist);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }


        /// <summary>
        /// Create a new record
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> InsertRecord([FromBody] LookupTableVM record)
        {
            var location = GetControllerActionNames();
            try
            {
                if (record is null)
                {
                    return BadRequest();
                }
                var insertedId = await _service.CriaNovoRegisto(record);
                if (insertedId > 0)
                {
                    int keyCreated = await _service.GetLastInsertedId(record.Tabela);
                    var createdRecord = _service.GetRecordById(keyCreated, record.Tabela);
                    var actionReturned = CreatedAtAction(nameof(GetRecordById), new { id = createdRecord.Id, tableName = record.Tabela }, createdRecord);
                    return actionReturned;
                }
                else
                {
                    return InternalError($"{location}: Erro no Api (InsertRecord");
                }
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }



        /// <summary>
        /// Update lookup table
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lookupTable"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateLookupTable(int id, [FromBody] LookupTableVM lookupTable)
        {
            var location = GetControllerActionNames();
            try
            {
                if (lookupTable == null)
                {
                    string msg = "A tabela passada como paràmetro é incorreta.";
                    _logger.LogWarning(msg);
                    return BadRequest(msg);
                }

                if (id != lookupTable.Id)
                {
                    return BadRequest($"O id ({id}) passado como paràmetro é incorreto");
                }
                if (string.IsNullOrEmpty(lookupTable.Tabela))
                {
                    return BadRequest($"O nome da tabela ({lookupTable.Tabela}) é incorreto");
                }

                var tableToVerify = await _service.GetRecordById(lookupTable.Id, lookupTable.Tabela);

                if (tableToVerify == null)
                {
                    return NotFound($"A tabela com o Id {id} não foi encontrada");
                }

                var tableToUpdate = await _service.ActualizaDetalhes(lookupTable);
                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        ///  Delete Lookup Record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}/{tableName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLookupRecord(int id, string tableName)
        {
            var location = GetControllerActionNames();
            try
            {
                if (id < 1)
                {
                    _logger.LogWarning($"{location}: Delete failed with bad data - id: {id}");
                    return BadRequest();
                }
                if (string.IsNullOrEmpty(tableName))
                {
                    _logger.LogWarning($"{location}: Delete failed with bad data - tableName: {tableName}");
                    return BadRequest();
                }

                var deleteOk = await _service.DeleteRegisto(id, tableName);
                if (!deleteOk)
                {
                    return InternalError($"{location}: Erro ao apagar registo. Contacte Administrador");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// CheckFkInUse
        /// </summary>
        /// <param name="idFK"></param>
        /// <param name="fieldToCheck"></param>
        /// <param name="tableToCheck"></param>
        /// <returns></returns>
        [HttpGet("CheckFkInUse/{idFK:int}/{fieldToCheck}/{tableToCheck}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CheckFkInuse(int idFK, string fieldToCheck, string tableToCheck)
        {
            var location = GetControllerActionNames();
            try
            {
                if (idFK < 1 || string.IsNullOrEmpty(fieldToCheck) || string.IsNullOrEmpty(tableToCheck))
                {
                    return BadRequest($"paràmetros em falta");
                }

                var fKInUse = await _service.CheckFKInUse(idFK, fieldToCheck, tableToCheck);
                return Ok(fKInUse);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Get Last InsertedId
        /// </summary>
        /// <param name="tableToCheck"></param>
        /// <returns></returns>
        [HttpGet("GetLastInsertedId/{tableToCheck}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetLastInsertedId(string tableToCheck)
        {
            var location = GetControllerActionNames();
            try
            {
                if (string.IsNullOrEmpty(tableToCheck))
                {
                    return BadRequest($"paràmetros em falta");
                }

                var lastId = await _service.GetLastInsertedId(tableToCheck);
                return Ok(lastId);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Get first id of a lookup table
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpGet("GetFirstId/{tableName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetFirstId(string tableName)
        {
            var location = GetControllerActionNames();
            try
            {
                if (string.IsNullOrEmpty(tableName))
                {
                    return BadRequest($"paràmetros em falta");
                }

                var firstId = await _service.GetFirstId(tableName);
                return Ok(firstId);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
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
