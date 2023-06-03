using DaisyPets.Core.Application.Exceptions;
using DaisyPets.Core.Application.Interfaces.Application;
using DaisyPets.Core.Application.Interfaces.DapperContext;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DaisyPets.Infrastructure.Repositories
{
    public class PetRepository : IPetRepository
    {
        DataAccessStatus dataAccessStatus = new DataAccessStatus();
        private readonly IDapperContext _context;
        private readonly ILogger<PetRepository> _logger;

        public PetRepository(IDapperContext context, ILogger<PetRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> InsertAsync(Pet pet)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO Pet (");
            sb.Append("Chip, Chipado, DataChip, NumeroChip, Cor, DoencaCronica, Esterilizado, Foto, IdEspecie, DataNascimento, ");
            sb.Append("Medicacao, IdPeso, IdRaca, IdTamanho, IdSituacao, IdTemperamento, Nome, ");
            sb.Append("Observacoes, Padrinho) VALUES(");
            sb.Append("@Chip, @Chipado, @DataChip, @NumeroChip, @Cor, @DoencaCronica, @Esterilizado, @Foto, @IdEspecie, @DataNascimento, ");
            sb.Append("@Medicacao, @IdPeso, @IdRaca, @IdTamanho, @IdSituacao, @IdTemperamento, @Nome, ");
            sb.Append("@Observacoes, @Padrinho");
            sb.Append("); ");
            sb.Append("SELECT last_insert_rowid()");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstAsync<int>(sb.ToString(), param: pet);
                    return result;
                }

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
                return -1;
            }
        }

        public async Task UpdateAsync(int Id, Pet pet)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", pet.Id);
            dynamicParameters.Add("@Chip", pet.Chip);
            dynamicParameters.Add("@Chipado", pet.Chipado);
            dynamicParameters.Add("@DataChip", pet.DataChip);
            dynamicParameters.Add("@NumeroChip", pet.NumeroChip);
            dynamicParameters.Add("@Cor", pet.Cor);
            dynamicParameters.Add("@Foto", pet.Foto);
            dynamicParameters.Add("@DoencaCronica", pet.DoencaCronica);
            dynamicParameters.Add("@Esterilizado", pet.Esterilizado);
            dynamicParameters.Add("@IdEspecie", pet.IdEspecie);
            dynamicParameters.Add("@DataNascimento", pet.DataNascimento);
            dynamicParameters.Add("@Medicacao", pet.Medicacao);
            dynamicParameters.Add("@IdPeso", pet.IdPeso);
            dynamicParameters.Add("@IdRaca", pet.IdRaca);
            dynamicParameters.Add("@IdTamanho", pet.IdTamanho);
            dynamicParameters.Add("@IdSituacao", pet.IdSituacao);
            dynamicParameters.Add("@IdTemperamento", pet.IdTemperamento);
            dynamicParameters.Add("@Nome", pet.Nome);
            dynamicParameters.Add("@Observacoes", pet.Observacoes);
            dynamicParameters.Add("@Padrinho", pet.Padrinho);

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE Pet SET ");
            sb.Append("Chip = @Chip, ");
            sb.Append("Chipado = @Chipado, ");
            sb.Append("DataChip = @DataChip, ");
            sb.Append("NumeroChip = @NumeroChip, ");
            sb.Append("Cor = @Cor, ");
            sb.Append("Foto = @Foto, ");
            sb.Append("DoencaCronica = @DoencaCronica, ");
            sb.Append("Esterilizado = @Esterilizado, ");
            sb.Append("IdEspecie = @IdEspecie, ");
            sb.Append("DataNascimento = @DataNascimento, ");
            sb.Append("IdTamanho = @IdTamanho, ");
            sb.Append("Medicacao = @Medicacao, ");
            sb.Append("IdPeso = @IdPeso, ");
            sb.Append("IdRaca = @IdRaca, ");
            sb.Append("IdSituacao = @IdSituacao, ");
            sb.Append("IdTemperamento = @IdTemperamento, ");
            sb.Append("Nome = @Nome, ");
            sb.Append("Observacoes = @Observacoes, ");
            sb.Append("Padrinho = @Padrinho ");
            sb.Append("WHERE Id = @Id");

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sb.ToString(), param: dynamicParameters);
            }

        }

        public async Task DeleteAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM Pet ");
            sb.Append("WHERE Id = @Id");
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sb.ToString(), new { Id });
            }
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Pet ");
            using (var connection = _context.CreateConnection())
            {
                var pets = await connection.QueryAsync<Pet>(sb.ToString());
                if (pets != null)
                {
                    return pets;
                }
                else
                {
                    return Enumerable.Empty<Pet>();
                }
            }

        }


        public async Task<Pet> FindByIdAsync(int Id)
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
                        return pet;
                    }
                    else
                    {
                        return new Pet();
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex);
                throw;
            }
        }

        public async Task<PetVM> GetPetVMAsync(int Id)
        {
            string sqlQuery = GetPetsVM_Query();

            StringBuilder sb = new StringBuilder();
            sb.Append(sqlQuery);
            sb.Append($" WHERE Pet.Id = @Id");

            using (var connection = _context.CreateConnection())
            {
                var petVM = await connection.QuerySingleOrDefaultAsync<PetVM>(sb.ToString(), new { Id });
                if (petVM != null)
                {
                    return petVM;
                }
                else
                {
                    return new PetVM();
                }
            }

        }

        public async Task<IEnumerable<Peso>> GetPesos()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Id, De, A, Descricao ");
            sb.Append("FROM Peso");

            using (var connection = _context.CreateConnection())
            {
                var pesos = await connection.QueryAsync<Peso>(sb.ToString());
                if (pesos != null)
                {
                    return pesos;
                }
                else
                {
                    return Enumerable.Empty<Peso>();
                }
            }
        }
        public async Task<string> GetDescriptionBySizeAndMonths(int IdTamanho, int meses)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Descricao ");
            sb.Append("FROM Idade ");
            sb.Append("WHERE IdTamanho = @IdTamanho AND ");
            sb.Append("@meses BETWEEN De AND A");

            using (var connection = _context.CreateConnection())
            {
                var ageDescription = await connection.QueryFirstAsync<string>(sb.ToString(), new {IdTamanho, meses});
                if (!string.IsNullOrEmpty(ageDescription))
                {
                    return ageDescription;
                }
                else
                {
                    return "";
                }
            }
        }

        public async Task<IEnumerable<PetVM>> GetAllVMAsync()
        {
            string sqlQuery = GetPetsVM_Query();

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var allPetsVM = await connection.QueryAsync<PetVM>(sqlQuery);
                    if (allPetsVM != null)
                    {
                        return allPetsVM;
                    }
                    else
                    {
                        return Enumerable.Empty<PetVM>();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        private string GetPetsVM_Query()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Pet.Id, Pet.Nome, Pet.DoencaCronica, Pet.Observacoes, Pet.Foto, ");
            sb.Append("Pet.Chip, Pet.Chipado, Pet.DataChip, Pet.NumeroChip,  Pet.Esterilizado, ");
            sb.Append("R.Descricao AS [RacaAnimal], Pet.Medicacao AS [MedicacaoAnimal], ");
            sb.Append("E.Descricao AS [EspecieAnimal], T.Descricao AS [TamanhoAnimal], ");
            sb.Append("S.Descricao AS [SituacaoAnimal] ");
            sb.Append("FROM Pet INNER JOIN Raca R ON Pet.IdRaca = R.Id ");
            sb.Append("INNER JOIN Especie E ON Pet.IdEspecie = E.Id ");
            sb.Append("INNER JOIN Tamanho T ON Pet.IdTamanho = T.Id ");
            sb.Append("INNER JOIN Situacao S ON Pet.IdSituacao = S.Id");

            return sb.ToString();
        }
    }
}
