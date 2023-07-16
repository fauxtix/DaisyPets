using DaisyPets.WebApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaisyPets.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUtilsController : ControllerBase
    {
        private  readonly IConfiguration _configuration;

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="configuration"></param>
        public AppUtilsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Backup Sqlite Db
        /// </summary>
        /// <returns></returns>
        [HttpGet("BackupSqlite")]
        public IActionResult SqliteBackup()
        {
            try
            {
                var _databaseName = _configuration?.GetConnectionString("PetsConnection") ?? "";

                UtilsService.BackupDatabase(_databaseName);
                return Ok();

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
