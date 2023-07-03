using DaisyPets.Core.Application.Exceptions;
using DaisyPets.Core.Application.Interfaces.DapperContext;
using DaisyPets.Core.Application.Interfaces.Repositories;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DaisyPets.Infrastructure.Repositories
{
    public class GaleriaFotosRepository : IGaleriaFotosRepository
    {
        DataAccessStatus dataAccessStatus = new DataAccessStatus();
        private readonly IDapperContext _context;
        private readonly ILogger<GaleriaFotosRepository> _logger;

        public GaleriaFotosRepository(IDapperContext context, ILogger<GaleriaFotosRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task DeleteAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM GaleriaFotos ");
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

        public async Task<GaleriaFotos> FindByIdAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM GaleriaFotos ");
            sb.Append($"WHERE Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var pet = await connection.QuerySingleOrDefaultAsync<GaleriaFotos>(sb.ToString(), new { Id });
                    if (pet != null)
                    {
                        return pet;
                    }
                    else
                    {
                        return new GaleriaFotos();
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex);
                throw;
            }
        }

        public async Task<IEnumerable<GaleriaFotos>> GetAllAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM GaleriaFotos ");
            using (var connection = _context.CreateConnection())
            {
                var photos = await connection.QueryAsync<GaleriaFotos>(sb.ToString());
                if (photos != null)
                {
                    return photos;
                }
                else
                {
                    return Enumerable.Empty<GaleriaFotos>();
                }
            }
        }

        public async Task<IEnumerable<GaleriaFotosVM>> GetAllPhotosVMAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT GaleriaFotos.Id, GaleriaFotos.IdPet, GaleriaFotos.Imagem, ");
            sb.Append("GaleriaFotos.Data, Pet.Nome AS NomePet ");
            sb.Append("FROM GaleriaFotos ");
            sb.Append("INNER JOIN Pet ON ");
            sb.Append("GaleriaFotos.IdPet = Pet.Id");


            using (var connection = _context.CreateConnection())
            {
                var photosVM = await connection.QueryAsync<GaleriaFotosVM>(sb.ToString());
                if (photosVM != null)
                {
                    return photosVM;
                }
                else
                {
                    return Enumerable.Empty<GaleriaFotosVM>();
                }
            }
        }

        public async Task<GaleriaFotosVM> GetPhotoVMAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT GaleriaFotos.Id, GaleriaFotos.IdPet, GaleriaFotos.Imagem, ");
            sb.Append("GaleriaFotos.Data, Pet.Nome AS NomePet ");
            sb.Append("FROM GaleriaFotos ");
            sb.Append("INNER JOIN Pet ON ");
            sb.Append("GaleriaFotos.IdPet = Pet.Id");
            sb.Append("WHERE GaleriaFotos.Id = @Id");


            using (var connection = _context.CreateConnection())
            {
                var photoVM = await connection.QueryFirstOrDefaultAsync<GaleriaFotosVM>(sb.ToString(), new { Id });
                if (photoVM != null)
                {
                    return photoVM;
                }
                else
                {
                    return new GaleriaFotosVM();
                }
            }
        }

        public async Task<int> InsertAsync(GaleriaFotos galeria)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO GaleriaFotos (");
            sb.Append("IdPet, Imagem, Data) ");
            sb.Append(" VALUES(");
            sb.Append("@IdPet, @Imagem, @Data");
            sb.Append(");");
            sb.Append("SELECT last_insert_rowid()");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstAsync<int>(sb.ToString(), param: galeria);
                    return result;
                }

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
                return -1;
            }

        }

        public async Task UpdateAsync(int Id, GaleriaFotos galeria)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", galeria.Id);
            dynamicParameters.Add("@IdPet", galeria.IdPet);
            dynamicParameters.Add("@Data", galeria.Data);
            dynamicParameters.Add("@Imagem", galeria.Imagem);

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE GaleriaImagens SET ");
            sb.Append("IdPet = @IdPet, ");
            sb.Append("Data = @Data, ");
            sb.Append("Imagem = @Imagem ");
            sb.Append("WHERE Id = @Id");

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sb.ToString(), param: dynamicParameters);
            }

        }
    }
}
