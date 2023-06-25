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
    public class VacinasRepository : IVacinasRepository
    {
        DataAccessStatus dataAccessStatus = new DataAccessStatus();
        private readonly IDapperContext _context;
        private readonly ILogger<ContactRepository> _logger;

        public VacinasRepository(IDapperContext context, ILogger<ContactRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task DeleteAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM Vacina ");
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

        public async Task<Vacina> FindByIdAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Vacina ");
            sb.Append($"WHERE Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var vacina = await connection.QuerySingleOrDefaultAsync<Vacina>(sb.ToString(), new { Id });
                    if (vacina != null)
                    {
                        return vacina;
                    }
                    else
                    {
                        return new Vacina();
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex);
                throw;
            }
        }

        public async Task<IEnumerable<Vacina>> GetAllAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Vacina ");
            using (var connection = _context.CreateConnection())
            {
                var contacts = await connection.QueryAsync<Vacina>(sb.ToString());
                if (contacts != null)
                {
                    return contacts;
                }
                else
                {
                    return Enumerable.Empty<Vacina>();
                }
            }
        }

        public async Task<IEnumerable<VacinaVM>> GetAllVacinasVMAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Vacina.Id, Vacina.IdPet, Vacina.DataToma, Vacina.Marca, ");
            sb.Append("Vacina.ProximaTomaEmMeses, Vacina.ProximaTomaEmMeses, ");
            sb.Append("Pet.Nome AS [NomePet] ");
            sb.Append("FROM Vacina ");
            sb.Append("INNER JOIN Pet ON ");
            sb.Append("Vacina.IdPet = Pet.Id");


            using (var connection = _context.CreateConnection())
            {
                var vacinasVM = await connection.QueryAsync<VacinaVM>(sb.ToString());
                if (vacinasVM != null)
                {
                    return vacinasVM;
                }
                else
                {
                    return Enumerable.Empty<VacinaVM>();
                }
            }
        }

        public async Task<IEnumerable<VacinaVM>> GetPetVaccinesVMAsync(int petId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Vacina.Id, Vacina.IdPet, Vacina.DataToma, Vacina.Marca, ");
            sb.Append("Vacina.ProximaTomaEmMeses, ");
            sb.Append("Pet.Nome AS [NomePet] ");
            sb.Append("FROM Vacina ");
            sb.Append("INNER JOIN Pet ON ");
            sb.Append("Vacina.IdPet = Pet.Id ");
            sb.Append("WHERE Vacina.IdPet = @PetId");


            using (var connection = _context.CreateConnection())
            {
                var vacinasVM = await connection.QueryAsync<VacinaVM>(sb.ToString(), new {PetId  = petId});
                if (vacinasVM != null)
                {
                    return vacinasVM;
                }
                else
                {
                    return Enumerable.Empty<VacinaVM>();
                }
            }
        }


        public async Task<VacinaVM> GetVacinaVMAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Vacina.Id, Vacina.IdPet, Vacina.DataToma, Vacina.Marca, ");
            sb.Append("Vacina.ProximaTomaEmMeses, Vacina.ProximaTomaEmMeses, ");
            sb.Append("Pet.Nome AS [NomePet] ");
            sb.Append("FROM Vacina ");
            sb.Append("INNER JOIN Pet ON ");
            sb.Append("Vacina.IdPet = Pet.Id ");
            sb.Append("WHERE Vacina.Id = @Id");

            using (var connection = _context.CreateConnection())
            {
                var vacinaVM = await connection.QueryFirstOrDefaultAsync<VacinaVM>(sb.ToString(), new { Id });
                if (vacinaVM != null)
                {
                    return vacinaVM;
                }
                else
                {
                    return new VacinaVM();
                }
            }
        }

        public async Task<int> InsertAsync(Vacina vacina)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO Vacina (");
            sb.Append("IdPet, DataToma, Marca, ProximaTomaEmMeses) ");
            sb.Append(" VALUES(");
            sb.Append("@IdPet, @DataToma, @Marca, @ProximaTomaEmMeses");
            sb.Append(");");
            sb.Append("SELECT last_insert_rowid()");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstAsync<int>(sb.ToString(), param: vacina);
                    return result;
                }

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
                return -1;
            }

        }

        public async Task UpdateAsync(int Id, Vacina vacina)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", vacina.Id);
            dynamicParameters.Add("@IdPet", vacina.IdPet);
            dynamicParameters.Add("@DataToma", vacina.DataToma);
            dynamicParameters.Add("@Marca", vacina.Marca);
            dynamicParameters.Add("@ProximaTomaEmMeses", vacina.ProximaTomaEmMeses);

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE Vacina SET ");
            sb.Append("IdPet = @IdPet, ");
            sb.Append("DataToma = @DataToma, ");
            sb.Append("Marca = @Marca, ");
            sb.Append("ProximaTomaEmMeses = @ProximaTomaEmMeses ");
            sb.Append("WHERE Id = @Id");

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sb.ToString(), param: dynamicParameters);
            }
        }


    }
}
