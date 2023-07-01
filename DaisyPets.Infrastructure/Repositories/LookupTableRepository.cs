using DaisyPets.Core.Application.Interfaces.Application;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Infrastructure.Context;
using Dapper;
using Microsoft.Extensions.Logging;

using System.Text;

namespace DaisyPets.Infrastructure.Repositories
{
    public class LookupTableRepository : ILookupTableRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<LookupTableRepository> _logger;

        public LookupTableRepository(DapperContext context, ILogger<LookupTableRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<LookUp>> GetLookupTableData(string tableName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Id, Descricao ");
            sql.Append($"FROM {tableName} ");
            sql.Append("ORDER BY Descricao");

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<LookUp>(sql.ToString());
                return result.ToList();
            }
        }

        public async Task< string> GetDescription(int id, string tableName)
        {
            DynamicParameters paramCollection = new DynamicParameters();
            paramCollection.Add("@Id", id);
            paramCollection.Add("@TableName", tableName);

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Descricao ");
            sql.Append("FROM @TableName WHERE Id = @Id ");
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<string>(sql.ToString());
                return result;
            }
        }

        public async Task<int> CriaNovoRegisto(LookUp tableRecord)
        {
            string descricao = "Descricao";

            DynamicParameters paramCollection = new DynamicParameters();
            paramCollection.Add("@Descricao", tableRecord.Descricao);

            StringBuilder sql = new StringBuilder();
            sql.Append($"INSERT INTO {tableRecord.Tabela} ({descricao})");
            sql.Append("VALUES (@Descricao); ");
            sql.Append("SELECT last_insert_rowid()");
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var output = await connection.QueryFirstAsync<int>(sql.ToString(),
                        param: paramCollection);
                    return output;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Atualiza Registo - genérico - tabelas auxiliares 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="Descricao"></param>
        /// <param name="Tabela"></param>
        /// <returns></returns>
        public async Task<bool> ActualizaDetalhes(LookUp tableRecord)
        {

            DynamicParameters paramCollection = new DynamicParameters();
            paramCollection.Add("@Id", tableRecord.Id);
            paramCollection.Add("@Description", tableRecord.Descricao);

            string Query = $"UPDATE {tableRecord.Tabela} SET Descricao = @Description WHERE Id = @Id";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    int i = await connection.ExecuteAsync(Query, paramCollection);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Check Registo Exist - genérico - tabelas auxiliares 
        /// </summary>
        /// <param name="Descricao"></param>
        /// <param name="Tabela"></param>
        /// <returns></returns>
        public async Task<bool> CheckRegistoExist(string Descricao, string Tabela)
        {
            string Query = string.Empty;
            int iCnt = 0;
            DynamicParameters paramCollection = new DynamicParameters();
            paramCollection.Add("@Descricao", Descricao);

            string descricao = "Descricao";

            Query = $"SELECT COUNT(*) FROM {Tabela} WHERE {descricao} = @Descricao";
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    iCnt = Convert.ToInt32(await connection.QueryFirstOrDefaultAsync<bool>(Query, paramCollection));
                    return (iCnt > 0);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Check record Exist in child tables => genérico - tabelas auxiliares 
        /// </summary>
        /// <param name="Descricao"></param>
        /// <param name="Tabela"></param>
        /// <returns></returns>
        public async Task<bool> CheckFKInUse(int sourceFk, string fieldToCheck, string tableToCheck)
        {
            string Query = string.Empty;
            int iCnt = 0;
            DynamicParameters paramCollection = new DynamicParameters();
            paramCollection.Add("@IdFk", sourceFk);

            Query = $"SELECT COUNT(*) FROM {tableToCheck} WHERE {fieldToCheck} = @IdFk";
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    iCnt = Convert.ToInt32(await connection.QueryFirstOrDefaultAsync<int>(Query, paramCollection));
                    return (iCnt > 0);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Delete record => genérico - tabelas auxiliares
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="Tabela"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRegisto(int Id, string tableName)
        {

            string Query = $"DELETE FROM {tableName} WHERE Id = @Id";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(Query, new { Id });
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Get IdByDescription/Table, de uma determinada tabela
        /// </summary>
        /// <param name="Descricao"></param>
        /// <param name="Tabela"></param>
        /// <returns></returns>
        public async Task<int> GetId(string descricao, string tabela)
        {
            DynamicParameters paramCollection = new DynamicParameters();
            paramCollection.Add("@Descricao", descricao);
            string Query = $"SELECT Id FROM {tabela} WHERE Descricao = @Descricao";

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

        public async Task<int> GetCodByDescricao(string sDescr, string sTabela)
        {
            string descricao = "Descricao";
            string codigo = "Id";

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append($"SELECT {codigo} ");
            sqlCommand.Append("FROM " + sTabela);
            sqlCommand.Append($" WHERE {descricao} LIKE '" + sDescr + "'");
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    return Convert.ToInt32(await connection.QueryFirstOrDefaultAsync<int>(sqlCommand.ToString()));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Get Descricao - genérico - tabelas auxiliares
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="Tabela"></param>
        /// <returns></returns>
        public async Task<string> GetDescricao(int Codigo, string Tabela)
        {
            string descricao = "Descricao";
            string codigo = "Id";

            DynamicParameters paramCollection = new DynamicParameters();
            paramCollection.Add("@Codigo", Codigo);

            string Query = $"SELECT {descricao} FROM {Tabela} WHERE {codigo} = @Codigo";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    return await connection.QueryFirstOrDefaultAsync<string>(Query, paramCollection);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "";
            }
        }

        /// <summary>
        /// Get Descrição with LIKE... (versão 2)
        /// </summary>
        /// <param name="Descricao"></param>
        /// <param name="sTabela">Tabela a pesquisar Descrição</param>
        /// <returns></returns>
        public async Task<IEnumerable<LookUp>> GetDataByDescricao(string sDescricao, string sTabela, bool TemAlertas = false)
        {
            string descricao = "Descricao";
            string codigo = "Id";

            DynamicParameters paramCollection = new DynamicParameters();
            paramCollection.Add("@Descricao", sDescricao);

            StringBuilder sqlCommand = new StringBuilder($"SELECT {codigo}, {descricao} ");
            if (TemAlertas)
                sqlCommand.Append(", Alerta ");

            sqlCommand.Append($"FROM {sTabela} ");

            if (sDescricao != string.Empty)
                sqlCommand.Append($" WHERE {descricao} LIKE + '%' + @Descricao + '%' ");

            sqlCommand.Append($" ORDER BY {descricao} ");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    return await connection.QueryAsync<LookUp>(sqlCommand.ToString(), paramCollection);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<LookUp>> GenericGetAll(string sTabela)
        {
            string descricao = "Descricao";
            string codigo = "Id";

            StringBuilder sb = new StringBuilder();
            //caso especial - especialidades
            sb.Append($"SELECT {codigo}, {descricao} ");

            sb.Append($"FROM {sTabela} ");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var lst = await connection.QueryAsync<LookUp>(sb.ToString());
                    return lst;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<LookUp>> GetDescricaoByDescricao(string sDescricao, string sTabela)
        {
            string descricao = "Descricao";
            string where = descricao;
            StringBuilder sqlCommand = new StringBuilder();

            sqlCommand.Append($"SELECT {descricao} ");

            sqlCommand.Append($"FROM {sTabela} ");
            sqlCommand.Append($"WHERE {descricao} LIKE '%{sDescricao}%' ");
            sqlCommand.Append("ORDER BY {descricao}");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    return await connection.QueryAsync<LookUp>(sqlCommand.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<LookUp>> GetDataFromTabela(string sTabela, string sDescricao = "")
        {
            string descricao = "Descricao";
            string codigo = "Id";

            DynamicParameters paramCollection = new DynamicParameters();
            paramCollection.Add("@Descricao", sDescricao);

            StringBuilder sqlCommand = new StringBuilder($"SELECT {codigo}, {descricao} ");

            sqlCommand.Append($"FROM {sTabela} ");

            if (sDescricao != string.Empty)
                sqlCommand.Append($" WHERE {descricao} LIKE + '%' + @Descricao + '%' ");

            sqlCommand.Append(" ORDER BY {descricao} ");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    return await connection.QueryAsync<LookUp>(sqlCommand.ToString(), paramCollection);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<int> GetLastInsertedId(string tableToCheck)
        {
            string id = "Id";

            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT TOP 1 {id} FROM {tableToCheck} ");
            sb.Append($"ORDER BY {id} DESC");
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var lastId = await connection.QueryFirstOrDefaultAsync<int>(sb.ToString());
                    return lastId;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return 0;
            }
        }

        public async Task<LookUp> GetRecordById(int id, string tableName)
        {
            DynamicParameters paramCollection = new DynamicParameters();
            paramCollection.Add("@Id", id);

            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT Id, Descricao FROM {tableName} ");
            sb.Append("WHERE Id = @Id");
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var record = await connection.QueryFirstOrDefaultAsync<LookUp>(sb.ToString(), param: paramCollection);
                    return record;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<int> GetFirstId(string tableName)
        {
            if (await TableHasData(tableName))
            {
                using (var connection = _context.CreateConnection())
                {
                    string sql = $"SELECT Id FROM {tableName} ORDER BY Id LIMIT 1";
                    int result = await connection.QueryFirstOrDefaultAsync<int>(sql);
                    return result;
                }
            }
            else
            {
                return 0;
            }
        }

        public async Task<bool> TableHasData(string tableName)
        {
            using (var connection = _context.CreateConnection())
            {
                string sql = $"SELECT COUNT(1) FROM {tableName}";
                int rows = await connection.QueryFirstOrDefaultAsync<int>(sql);
                return rows > 0;
            }
        }
    }
}
