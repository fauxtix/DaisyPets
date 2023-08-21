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
    public class ConsultaRepository : IConsultaRepository
    {
        DataAccessStatus dataAccessStatus = new DataAccessStatus();
        private readonly IDapperContext _context;
        private readonly ILogger<ConsultaRepository> _logger;

        public ConsultaRepository(IDapperContext context, ILogger<ConsultaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> InsertAsync(ConsultaVeterinario Consulta)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO ConsultaVeterinario (");
            sb.Append("DataConsulta, Motivo, Diagnostico, Tratamento, Notas, IdPet) ");
            sb.Append(" VALUES(");
            sb.Append("@DataConsulta, @Motivo, @Diagnostico, @Tratamento, @Notas, @IdPet");
            sb.Append(");");
            sb.Append("SELECT last_insert_rowid()");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstAsync<int>(sb.ToString(), param: Consulta);
                    return result;
                }

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
                return -1;
            }

        }

        public async Task UpdateAsync(int Id, ConsultaVeterinario Consulta)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", Consulta.Id);
            dynamicParameters.Add("@DataConsulta", Consulta.DataConsulta);
            dynamicParameters.Add("@Motivo", Consulta.Motivo);
            dynamicParameters.Add("@Diagnostico", Consulta.Diagnostico);
            dynamicParameters.Add("@Tratamento", Consulta.Tratamento);
            dynamicParameters.Add("@Notas", Consulta.Notas);
            dynamicParameters.Add("@IdPet", Consulta.IdPet);

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE ConsultaVeterinario SET ");
            sb.Append("DataConsulta = @DataConsulta, ");
            sb.Append("Motivo = @Motivo, ");
            sb.Append("Diagnostico = @Diagnostico, ");
            sb.Append("Tratamento = @Tratamento, ");
            sb.Append("Notas = @Notas, ");
            sb.Append("IdPet = @IdPet ");
            sb.Append("WHERE Id = @Id");

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sb.ToString(), param: dynamicParameters);
            }

        }

        public async Task DeleteAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM ConsultaVeterinario ");
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

        public async Task<ConsultaVeterinario> FindByIdAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ConsultaVeterinario ");
            sb.Append($"WHERE Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var pet = await connection.QuerySingleOrDefaultAsync<ConsultaVeterinario>(sb.ToString(), new { Id });
                    if (pet != null)
                    {
                        return pet;
                    }
                    else
                    {
                        return new ConsultaVeterinario();
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex);
                throw;
            }
        }

        public async Task<IEnumerable<ConsultaVeterinario>> GetAllAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ConsultaVeterinario ");
            using (var connection = _context.CreateConnection())
            {
                var Consultas = await connection.QueryAsync<ConsultaVeterinario>(sb.ToString());
                if (Consultas != null)
                {
                    return Consultas;
                }
                else
                {
                    return Enumerable.Empty<ConsultaVeterinario>();
                }
            }
        }

        public async Task<IEnumerable<ConsultaVeterinarioVM>> GetAllConsultaVMAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ConsultaVeterinario.Id, DataConsulta, Motivo, ");
            sb.Append("Diagnostico, Tratamento, IdPet, Pet.Nome AS [NomePet] ");
            sb.Append("FROM ConsultaVeterinario ");
            sb.Append("INNER JOIN Pet ON ");
            sb.Append("ConsultaVeterinario.IdPet = Pet.Id");


            using (var connection = _context.CreateConnection())
            {
                var ConsultasVM = await connection.QueryAsync<ConsultaVeterinarioVM>(sb.ToString());
                if (ConsultasVM != null)
                {
                    return ConsultasVM;
                }
                else
                {
                    return Enumerable.Empty<ConsultaVeterinarioVM>();
                }
            }
        }

        public async Task<IEnumerable<ConsultaVeterinarioVM>> GetConsultaVMAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ConsultaVeterinario.Id, DataConsulta, Motivo, ");
            sb.Append("Diagnostico, Tratamento, Notas, ");
            sb.Append("IdPet, Pet.Nome AS [NomePet] ");
            sb.Append("FROM ConsultaVeterinario ");
            sb.Append("INNER JOIN Pet ON ");
            sb.Append("ConsultaVeterinario.IdPet = Pet.Id ");
            sb.Append("WHERE ConsultaVeterinario.IdPet = @Id");


            using (var connection = _context.CreateConnection())
            {
                var ConsultaVM = await connection.QueryAsync<ConsultaVeterinarioVM>(sb.ToString(), new { Id });
                if (ConsultaVM != null)
                {
                    return ConsultaVM;
                }
                else
                {
                    return Enumerable.Empty<ConsultaVeterinarioVM>();
                }
            }
        }

    }
}
