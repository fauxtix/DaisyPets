using Dapper;
using MauiPets.Core.Domain;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Text;

namespace MauiPetsApp.Infrastructure
{
    public class VacinasRepository : IVacinasRepository
    {
        private readonly IDapperContext _context;
        private readonly ILogger<VacinasRepository> _logger;
        public VacinasRepository(IDapperContext context, ILogger<VacinasRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> InsertAsync(Vacina vacina)
        {
            var petName = await GetPetName(vacina.IdPet);
            var description = $"{petName} - Vacina da {vacina.Marca}";
            var categoryId = await GetVaccineTodoCategoryId("Vacinação");
            var startDate = DateTime.Parse(vacina.DataToma).ToShortDateString();
            var endDate = DateTime.Parse(vacina.DataToma).AddMonths(vacina.ProximaTomaEmMeses).ToShortDateString();
            int result;

            //ToDo toDo = new ToDo()
            //{
            //    CategoryId = categoryId,
            //    Description = description,
            //    StartDate = startDate,
            //    EndDate = endDate,
            //    Completed = 0,
            //    Generated = 1
            //};

            StringBuilder sb = new StringBuilder();
            StringBuilder sbTodoList = new StringBuilder();

            sb.Append("INSERT INTO Vacina (");
            sb.Append("IdPet, IdTipoVacina,  DataToma, Marca, ProximaTomaEmMeses) ");
            sb.Append(" VALUES(");
            sb.Append("@IdPet, @IdTipoVacina, @DataToma, @Marca, @ProximaTomaEmMeses");
            sb.Append(");");
            sb.Append("SELECT last_insert_rowid()");

            sbTodoList.Append("INSERT INTO ToDo( ");
            sbTodoList.Append("Description, StartDate, EndDate, Completed, CategoryId) ");
            sbTodoList.Append(" VALUES(");
            sbTodoList.Append("@Description, @StartDate, @EndDate, @Completed, @CategoryId");
            sbTodoList.Append(");");

            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        //await connection.ExecuteAsync(sbTodoList.ToString(), param: toDo, transaction: transaction);

                        result = await connection.QueryFirstAsync<int>(sb.ToString(), param: vacina, transaction: transaction);

                        transaction.Commit();
                        return result;

                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.ToString());
                        transaction.Rollback();
                        return -1;
                    }
                }
            }
        }

        public async Task UpdateAsync(int Id, Vacina vacina)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", vacina.Id);
            dynamicParameters.Add("@IdPet", vacina.IdPet);
            dynamicParameters.Add("@IdTipoVacina", vacina.IdTipoVacina);
            dynamicParameters.Add("@DataToma", vacina.DataToma);
            dynamicParameters.Add("@Marca", vacina.Marca);
            dynamicParameters.Add("@ProximaTomaEmMeses", vacina.ProximaTomaEmMeses);

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE Vacina SET ");
            sb.Append("IdPet = @IdPet, ");
            sb.Append("IdTipoVacina = @IdTipoVacina, ");
            sb.Append("DataToma = @DataToma, ");
            sb.Append("Marca = @Marca, ");
            sb.Append("ProximaTomaEmMeses = @ProximaTomaEmMeses ");
            sb.Append("WHERE Id = @Id");

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sb.ToString(), param: dynamicParameters);
            }
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
                Log.Error(ex.ToString());
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
                Log.Error(ex.ToString(), ex);
                return new Vacina();
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
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT Vacina.Id, Vacina.IdPet, Vacina.DataToma, Vacina.Marca, ");
                sb.Append("Vacina.ProximaTomaEmMeses, ");
                sb.Append("Pet.Nome AS [NomePet], ");
                sb.Append("TipoVacinas.Vacina AS [NomeTipoVacina] ");
                sb.Append("FROM Vacina ");
                sb.Append("INNER JOIN Pet ON ");
                sb.Append("Vacina.IdPet = Pet.Id ");
                sb.Append("INNER JOIN TipoVacinas ON ");
                sb.Append("Vacina.IdTipoVacina = TipoVacinas.Id");


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
            catch (Exception ex)
            {
                Log.Error(ex.ToString(), ex);
                return Enumerable.Empty<VacinaVM>();
            }
        }

        public async Task<IEnumerable<VacinaVM>> GetPetVaccinesVMAsync(int petId)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT Vacina.Id, Vacina.IdPet, Vacina.DataToma, Vacina.Marca, ");
                sb.Append("Vacina.ProximaTomaEmMeses, ");
                sb.Append("Pet.Nome AS [NomePet], ");
                sb.Append("TipoVacinas.Vacina AS [NomeTipoVacina] ");
                sb.Append("FROM Vacina ");
                sb.Append("INNER JOIN Pet ON ");
                sb.Append("Vacina.IdPet = Pet.Id ");
                sb.Append("INNER JOIN TipoVacinas ON ");
                sb.Append("Vacina.IdTipoVacina = TipoVacinas.Id ");
                sb.Append("WHERE Vacina.IdPet = @PetId");


                using (var connection = _context.CreateConnection())
                {
                    var vacinasVM = await connection.QueryAsync<VacinaVM>(sb.ToString(), new { PetId = petId });
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
            catch (Exception ex)
            {
                Log.Error(ex.ToString(), ex);
                return Enumerable.Empty<VacinaVM>();
            }
        }


        public async Task<VacinaVM> GetVacinaVMAsync(int vaccineId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Vacina.Id, Vacina.IdPet, Vacina.DataToma, Vacina.Marca, ");
            sb.Append("Vacina.ProximaTomaEmMeses, ");
            sb.Append("Pet.Nome AS [NomePet], ");
            sb.Append("TipoVacinas.Vacina AS [NomeTipoVacina] ");
            sb.Append("FROM Vacina ");
            sb.Append("INNER JOIN Pet ON ");
            sb.Append("Vacina.IdPet = Pet.Id ");
            sb.Append("INNER JOIN TipoVacinas ON ");
            sb.Append("Vacina.IdTipoVacina = TipoVacinas.Id ");
            sb.Append("WHERE Vacina.Id = @VaccineId");

            using (var connection = _context.CreateConnection())
            {
                var vacinaVM = await connection.QueryFirstOrDefaultAsync<VacinaVM>(sb.ToString(), new { VaccineId = vaccineId });
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
                Log.Error(ex.ToString(), ex);
                return "";

            }
        }
        private async Task<int> GetVaccineTodoCategoryId(string descricao)
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
                Log.Error(ex.Message);
                return 0;
            }
        }

        public async Task<IEnumerable<TipoVacina>> GetTipoVacinasAsync(int specieId)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    StringBuilder sb = new();
                    sb.Append("SELECT Id, Id_Especie,  Categoria, Vacina, Prevencao, Notas ");
                    sb.Append("FROM TipoVacinas  ");
                    sb.Append("WHERE Id_Especie = @Specie");

                    var vacinas = await connection.QueryAsync<TipoVacina>(
                        sb.ToString(), new { Specie = specieId });

                    return vacinas.ToList();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<TipoVacina>();
            }
        }


    }
}
