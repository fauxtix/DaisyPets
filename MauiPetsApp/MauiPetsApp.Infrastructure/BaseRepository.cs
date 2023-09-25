using MauiPetsApp.Infrastructure.Context;
using Dapper;
using MauiPetsApp.Core.Application.Exceptions;
using MauiPetsApp.Core.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Reflection;
using System.Text;

namespace MauiPetsApp.Infrastructure
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        DataAccessStatus? dataAccessStatus = new DataAccessStatus();
        private readonly DapperContext _context;
        private readonly ILogger? _logger;

        public BaseRepository()
        {

        }
        private IEnumerable<string> GetColumns()
        {
            return typeof(T)
                    .GetProperties()
                    .Where(e => e.Name != "Id" && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name);
        }

        public async Task<long> Insert(T entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns);
            var stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));

            StringBuilder? sbInsert = new StringBuilder();
            sbInsert.Append($"INSERT INTO {typeof(T).Name} ");
            sbInsert.Append($"({stringOfColumns}) ");
            sbInsert.Append($"VALUES ({stringOfParameters})");

            string query = sbInsert.ToString();

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, entity);
                }

                return await GetLastId();
            }
            catch (DataAccessException ex)
            {
                ex.DataAccessStatusInfo.Status = "Erro";
                ex.DataAccessStatusInfo.OperationSucceeded = false;
                ex.DataAccessStatusInfo.CustomMessage = $"Erro na inserção de registo (tabela '{typeof(T).Name}').";
                ex.DataAccessStatusInfo.ExceptionMessage = ex.Message;
                ex.DataAccessStatusInfo.StackTrace = ex.StackTrace; ;
                throw ex;
            }
        }

        public async Task Delete(T entity)
        {
            var query = $"DELETE FROM {typeof(T).Name} WHERE Id = @Id";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, entity);
                }
            }
            catch (DataAccessException ex)
            {
                ex.DataAccessStatusInfo.Status = "Erro";
                ex.DataAccessStatusInfo.OperationSucceeded = false;
                ex.DataAccessStatusInfo.CustomMessage = $"Erro na anulação de registo (tabela '{typeof(T).Name}').";
                ex.DataAccessStatusInfo.ExceptionMessage = ex.Message;
                ex.DataAccessStatusInfo.StackTrace = ex.StackTrace;
                throw ex;
            }
        }

        public async Task Update(T entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns.Select(e => $"{e} = @{e}"));
            var query = $"UPDATE {typeof(T).Name} SET {stringOfColumns} WHERE Id = @Id";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, entity);
                }
            }
            catch (DataAccessException ex)
            {
                ex.DataAccessStatusInfo.Status = "Erro";
                ex.DataAccessStatusInfo.OperationSucceeded = false;
                ex.DataAccessStatusInfo.CustomMessage = $"Erro na atualização de registo (tabela '{typeof(T).Name}').";
                ex.DataAccessStatusInfo.ExceptionMessage = ex.Message;
                ex.DataAccessStatusInfo.StackTrace = ex.StackTrace;
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> Query(string where = null)
        {
            var query = $"SELECT * FROM {typeof(T).Name} ";

            if (!string.IsNullOrWhiteSpace(where))
            {
                query += $"WHERE {where}";
            }

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<T>(query);
            }
        }

        public async Task<T> Query_ById(int Id)
        {

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstAsync<T>($"SELECT * FROM {typeof(T).Name} WHERE Id=@Id",
                    new { Id });
            }
        }

        public async Task<bool> EntradaExiste_BD(string campo, string str)
        {
            bool EntryExists = false;
            using (var connection = _context.CreateConnection())
            {
                EntryExists = await connection.QueryFirstOrDefaultAsync<bool>($"{campo} = '{str}'");
                return EntryExists;

                //    var output = Query($"{campo} = '{str}'");
                //    if (output.Count() > 0)
                //        return true;
                //}
                //return false;
            }
        }

        public async Task<bool> TableHasData()
        {
            using (var connection = _context.CreateConnection())
            {
                string sql = $"SELECT COUNT(1) FROM {typeof(T).Name}";
                int rows = await connection.QueryFirstOrDefaultAsync<int>(sql);
                return rows > 0;
            }
        }

        public async Task<int> GetFirstId()
        {
            if (await TableHasData())
            {
                using (var connection = _context.CreateConnection())
                {
                    string sql = $"SELECT Id FROM {typeof(T).Name} ORDER BY Id LIMIT 1";
                    int result = await connection.QueryFirstOrDefaultAsync<int>(sql);

                    // Funciona, desde que T venha ordenado por Id...
                    //int result = connection.Query<int>(sql).FirstOrDefault();
                    return result;
                }
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> GetLastId()
        {
            if (await TableHasData())
            {
                using (var connection = _context.CreateConnection())
                {
                    string sql = $"SELECT Id FROM {typeof(T).Name} ORDER BY Id DESC LIMIT 1";
                    int result = await connection.QueryFirstOrDefaultAsync<int>(sql);
                    return result;
                }
            }
            else
            {
                return 0;
            }
        }

        public async Task<bool> RecInUse(int Id)
        {
            var query = $"SELECT COUNT(1) FROM {typeof(T).Name} WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                bool exists = await connection.QueryFirstOrDefaultAsync<bool>(query, new { Id });
                return exists;
            }
        }

        public async Task UpdateById(int Id)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns.Select(e => $"{e} = @{e}"));
            var query = $"UPDATE {typeof(T).Name} SET {stringOfColumns} WHERE Id = @Id";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, new { Id });
                }
            }
            catch (DataAccessException ex)
            {
                ex.DataAccessStatusInfo.Status = "Erro";
                ex.DataAccessStatusInfo.OperationSucceeded = false;
                ex.DataAccessStatusInfo.CustomMessage = $"Erro na atualização de registo (tabela '{typeof(T).Name}').";
                ex.DataAccessStatusInfo.ExceptionMessage = ex.Message;
                ex.DataAccessStatusInfo.StackTrace = ex.StackTrace;
                throw ex;
            }
        }

        public async Task DeleteById(int Id)
        {
            var query = $"DELETE FROM {typeof(T).Name} WHERE Id = @Id";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, new { Id });
                }
            }
            catch (DataAccessException ex)
            {
                ex.DataAccessStatusInfo.Status = "Erro";
                ex.DataAccessStatusInfo.OperationSucceeded = false;
                ex.DataAccessStatusInfo.CustomMessage = $"Erro na anulação - tabela '{typeof(T).Name}'.";
                ex.DataAccessStatusInfo.ExceptionMessage = ex.Message;
                ex.DataAccessStatusInfo.StackTrace = ex.StackTrace;
                throw ex;
            }
        }
    }
}
