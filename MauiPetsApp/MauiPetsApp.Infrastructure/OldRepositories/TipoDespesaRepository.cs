using Dapper;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Domain;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MauiPetsApp.Infrastructure.OldRepositories
{
    public class TipoDespesaRepository : ITipoDespesaRepository
    {
        private readonly IDapperContext _context;
        private readonly ILogger<TipoDespesaRepository> _logger;
        private StringBuilder sb = new StringBuilder();

        public TipoDespesaRepository(IDapperContext context, ILogger<TipoDespesaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> ApagaTipoDespesa(int id)
        {
            sb.Clear();
            sb.Append("DELETE FROM  TipoDespesa ");
            sb.Append("WHERE Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.ExecuteAsync(sb.ToString(),
                            new { Id = id });
                    return true;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> AtualizaTipoDespesa(TipoDespesa alteraTipoDespesa)
        {
            DynamicParameters paramCollection = new DynamicParameters();

            paramCollection.Add("@Id", alteraTipoDespesa.Id);
            paramCollection.Add("@Descricao", alteraTipoDespesa.Descricao);
            paramCollection.Add("@IdCategoriaDespesa", alteraTipoDespesa.IdCategoriaDespesa); ;

            sb.Clear();
            sb.Append("UPDATE TipoDespesa ");
            sb.Append("SET [Descricao] = @Descricao, [IdCategoriaDespesa] = @IdCategoriaDespesa ");
            sb.Append("WHERE Id = @Id");
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(sb.ToString(), paramCollection);
                }

                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public async Task<bool> CanRecordBeDeleted(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                //parameters.Add("@OkToDelete", SqlDbType.Bit, direction: ParameterDirection.Output);

                sb.Clear();
                //sb.Append("DECLARE @CanDelete bit; ");
                sb.Append("SELECT COUNT(1) FROM    Despesa ");
                sb.Append("WHERE IDTipoDespesa = @Id; ");
                //sb.Append("SELECT @OkToDelete = @CanDelete");

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QuerySingleOrDefaultAsync<int>(sb.ToString(),
                        param: parameters);
                    return result == 0;
                    //var areThereExpenses = parameters.Get<int>("@OkToDelete");
                    //return areThereExpenses == 0;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }

        }

        public async Task<IEnumerable<TipoDespesa>?> GetAll()
        {
            sb.Clear();
            sb.Append("SELECT TD.Id, TD.Descricao, TD.IdCategoriaDespesa ");
            sb.Append("FROM  TipoDespesa TD ");
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var lst = await connection.QueryAsync<TipoDespesa>(sb.ToString());

                    return lst;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public async Task<IEnumerable<TipoDespesaVM>?> GetAllVM()
        {
            sb.Clear();
            sb.Append("SELECT TD.Id, TD.Descricao, TD.IdCategoriaDespesa, CD.Descricao CategoriaDespesa ");
            sb.Append("FROM  TipoDespesa TD ");
            sb.Append("INNER JOIN CategoriaDespesa CD ON ");
            sb.Append("TD.IdCategoriaDespesa = CD.Id");
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var lst = await connection.QueryAsync<TipoDespesaVM>(sb.ToString());

                    return lst;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public async Task<TipoDespesa?> GetTipoDespesa_ById(int Id)
        {
            sb.Clear();
            sb.Append("SELECT TD.Id, TD.Descricao, TD.IdCategoriaDespesa  ");
            sb.Append("FROM  TipoDespesa TD ");
            sb.Append("WHERE TD.Id = @Id");
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var lst = await connection.QueryFirstOrDefaultAsync<TipoDespesa>(sb.ToString(), new { Id });

                    return lst;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public async Task<TipoDespesaVM?> GetTipoDespesaVM_ById(int Id)
        {
            sb.Clear();
            sb.Append("SELECT TD.Id, TD.Descricao, TD.IdCategoriaDespesa, CD.Descricao CategoriaDespesa ");
            sb.Append("FROM  TipoDespesa TD ");
            sb.Append("INNER JOIN CategoriaDespesa CD ON ");
            sb.Append("TD.IdCategoriaDespesa = CD.Id ");
            sb.Append("WHERE TD.Id = @Id");
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var lst = await connection.QueryFirstOrDefaultAsync<TipoDespesaVM>(sb.ToString(), new { Id });

                    return lst;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }


        public async Task<int> InsereTipoDespesa(TipoDespesa novoTipoDespesa)
        {
            DynamicParameters paramCollection = new DynamicParameters();

            paramCollection.Add("@Descricao", novoTipoDespesa.Descricao);
            paramCollection.Add("@Id_CategoriaDespesa", novoTipoDespesa.IdCategoriaDespesa);

            sb.Clear();
            sb.Append("INSERT INTO TipoDespesa(");
            sb.Append("Descricao, [IdCategoriaDespesa]) ");
            sb.Append("VALUES (@Descricao, @Id_CategoriaDespesa); ");
            sb.Append("SELECT last_insert_rowid()");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    int insertedId = await connection.QueryFirstAsync<int>(sb.ToString(),
                            paramCollection);
                    return insertedId;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return -1;
            }
        }

        public async Task<IEnumerable<TipoDespesa>?> GetTipoDespesa_ByCategoria(int categoria)
        {
            sb.Clear();
            sb.Append("SELECT TD.Id, TD.Descricao  ");
            sb.Append("FROM  TipoDespesa TD ");
            sb.Append("WHERE TD.IdCategoriaDespesa = @Id");
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var lst = await connection
                        .QueryAsync<TipoDespesa>(sb.ToString(), new { Id = categoria });
                    return lst;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }

        }
    }
}
