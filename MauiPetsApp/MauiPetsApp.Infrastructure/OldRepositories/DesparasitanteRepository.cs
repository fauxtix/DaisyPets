using MauiPetsApp.Core.Application.Exceptions;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using MauiPetsApp.Core.Domain.TodoManager;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MauiPetsApp.Infrastructure.OldRepositories
{
    public class DesparasitanteRepository : IDesparasitanteRepository
    {
        DataAccessStatus dataAccessStatus = new DataAccessStatus();
        private readonly IDapperContext _context;
        private readonly ILogger<DesparasitanteRepository> _logger;

        public DesparasitanteRepository(IDapperContext context, ILogger<DesparasitanteRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> InsertAsync(Desparasitante desparasitante)
        {
            var petName = await GetPetName(desparasitante.IdPet);
            var description = $"{petName} - Desparasitante {desparasitante.Marca}";
            var categoryId = await GetDewormerTodoCategoryId("Med");
            var startDate = DateTime.Parse(desparasitante.DataAplicacao).ToShortDateString();
            var endDate = DateTime.Parse(desparasitante.DataProximaAplicacao).ToShortDateString();
            int result;

            StringBuilder sb = new StringBuilder();
            StringBuilder sbTodoList = new StringBuilder();


            sb.Append("INSERT INTO Desparasitante (");
            sb.Append("Tipo, Marca, DataAplicacao, DataProximaAplicacao, IdPet) ");
            sb.Append(" VALUES(");
            sb.Append("@Tipo, @Marca, @DataAplicacao, @DataProximaAplicacao, @IdPet");
            sb.Append(");");
            sb.Append("SELECT last_insert_rowid()");

            sbTodoList.Append("INSERT INTO ToDo( ");
            sbTodoList.Append("Description, StartDate, EndDate, Completed, CategoryId) ");
            sbTodoList.Append(" VALUES(");
            sbTodoList.Append("@Description, @StartDate, @EndDate, @Completed, @CategoryId");
            sbTodoList.Append(");");

            ToDo toDo = new ToDo()
            {
                CategoryId = categoryId,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                Completed = 0,
                Generated = 1
            };

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await connection.ExecuteAsync(sbTodoList.ToString(), param: toDo, transaction: transaction);

                        result = await connection.QueryFirstAsync<int>(sb.ToString(), param: desparasitante);
                        transaction.Commit();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        _logger.Log(LogLevel.Error, ex.ToString());
                        transaction.Rollback();
                        return -1;
                    }
                }
            }
        }


        public async Task UpdateAsync(int Id, Desparasitante desparasitante)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", desparasitante.Id);
            dynamicParameters.Add("@DataAplicacao", desparasitante.DataAplicacao);
            dynamicParameters.Add("@DataProximaAplicacao", desparasitante.DataProximaAplicacao);
            dynamicParameters.Add("@Marca", desparasitante.Marca);
            dynamicParameters.Add("@Tipo", desparasitante.Tipo);
            dynamicParameters.Add("@IdPet", desparasitante.IdPet);

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE Desparasitante SET ");
            sb.Append("DataAplicacao = @DataAplicacao, ");
            sb.Append("DataProximaAplicacao = @DataProximaAplicacao, ");
            sb.Append("Marca = @Marca, ");
            sb.Append("Tipo = @Tipo, ");
            sb.Append("IdPet = @IdPet ");
            sb.Append("WHERE Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(sb.ToString(), param: dynamicParameters);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task DeleteAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM Desparasitante ");
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

        public async Task<Desparasitante> FindByIdAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Desparasitante ");
            sb.Append($"WHERE Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var desparasitante = await connection.QuerySingleOrDefaultAsync<Desparasitante>(sb.ToString(), new { Id });
                    if (desparasitante != null)
                    {
                        return desparasitante;
                    }
                    else
                    {
                        return new Desparasitante();
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex);
                throw;
            }
        }

        public async Task<IEnumerable<Desparasitante>> GetAllAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Desparasitante ");
            using (var connection = _context.CreateConnection())
            {
                var desparasitantes = await connection.QueryAsync<Desparasitante>(sb.ToString());
                if (desparasitantes != null)
                {
                    return desparasitantes;
                }
                else
                {
                    return Enumerable.Empty<Desparasitante>();
                }
            }
        }

        public async Task<IEnumerable<DesparasitanteVM>> GetAllDesparasitantesVMAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Desparasitante.Id, DataAplicacao, DataProximaAplicacao, ");
            sb.Append("Marca, Tipo, IdPet, Pet.Nome AS [NomePet] ");
            sb.Append("FROM Desparasitante ");
            sb.Append("INNER JOIN Pet ON ");
            sb.Append("Desparasitante.IdPet = Pet.Id ");


            using (var connection = _context.CreateConnection())
            {
                var desparasitantesVM = await connection.QueryAsync<DesparasitanteVM>(sb.ToString());
                if (desparasitantesVM != null)
                {
                    return desparasitantesVM;
                }
                else
                {
                    return Enumerable.Empty<DesparasitanteVM>();
                }
            }
        }

        public async Task<IEnumerable<DesparasitanteVM>> GetDesparasitanteVMAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Desparasitante.Id, DataAplicacao, DataProximaAplicacao, Marca, Tipo, IdPet, Pet.Nome AS [NomePet] ");
            sb.Append("FROM Desparasitante ");
            sb.Append("INNER JOIN Pet ON ");
            sb.Append("Desparasitante.IdPet = Pet.Id ");
            sb.Append("WHERE Desparasitante.IdPet = @Id");


            using (var connection = _context.CreateConnection())
            {
                var desparasitanteVM = await connection.QueryAsync<DesparasitanteVM>(sb.ToString(), new { Id });
                if (desparasitanteVM != null)
                {
                    return desparasitanteVM;
                }
                else
                {
                    return Enumerable.Empty<DesparasitanteVM>();
                }
            }
        }

        public async Task<string> GetPetName(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Pet ");
            sb.Append($"WHERE Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var pet = await connection.QuerySingleOrDefaultAsync<Pet>(sb.ToString(), new { Id });
                    if (pet != null)
                    {
                        return pet.Nome;
                    }
                    else
                    {
                        return "";
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex);
                throw;
            }
        }
        private async Task<int> GetDewormerTodoCategoryId(string descricao)
        {
            DynamicParameters paramCollection = new DynamicParameters();
            paramCollection.Add("@Descricao", descricao);
            string Query = $"SELECT Id FROM ToDoCategories WHERE Descricao LIKE '{descricao}%'";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    return await connection.QueryFirstOrDefaultAsync<int>(Query, paramCollection);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return 0;
            }
        }

    }
}
