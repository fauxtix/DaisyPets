﻿using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Core.Domain;
using DaisyPets.Infrastructure.Context;
using Dapper;
using Microsoft.Extensions.Logging;
using PropertyManagerFL.Application.Interfaces.Repositories;

using System.Data;
using System.Text;

namespace PropertyManagerFL.Infrastructure.Repositories
{
    public class DespesaRepository : IDespesaRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<DespesaRepository> _logger;

        public DespesaRepository(DapperContext context, ILogger<DespesaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> InsertAsync(Despesa expense)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO Despesa (");
            sb.Append("DataMovimento, ValorPago, NumeroDocumento, IdTipoDespesa, IdCategoriaDespesa, Notas) ");
            sb.Append(" VALUES(");
            sb.Append("@DataMovimento, @ValorPago, @NumeroDocumento, @IdTipoDespesa, @IdCategoriaDespesa, @Notas");
            sb.Append(");");
            sb.Append("SELECT last_insert_rowid()");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstAsync<int>(sb.ToString(), param: expense);
                    return result;
                }

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
                return -1;
            }
        }


        public async Task<bool> UpdateAsync(int id, Despesa expense)
        {

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", expense.Id);
            dynamicParameters.Add("@DataMovimento", expense.DataMovimento);
            dynamicParameters.Add("@ValorPago", expense.ValorPago);
            dynamicParameters.Add("@NumeroDocumento", expense.NumeroDocumento);
            dynamicParameters.Add("@IdTipoDespesa", expense.IdTipoDespesa);
            dynamicParameters.Add("@IdCategoriaDespesa", expense.IdCategoriaDespesa);
            dynamicParameters.Add("@Notas", expense.Notas);

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE Despesa SET ");
            sb.Append("DataMovimento = @DataMovimento, ");
            sb.Append("ValorPago = @ValorPago, ");
            sb.Append("NumeroDocumento = @NumeroDocumento, ");
            sb.Append("IdTipoDespesa = @IdTipoDespesa, ");
            sb.Append("IdCategoriaDespesa = @IdCategoriaDespesa, ");
            sb.Append("Notas = @Notas");
            sb.Append("WHERE Id = @Id");


            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var updateOk = await connection.QueryFirstAsync<bool>(sb.ToString(), param: dynamicParameters);
                    return updateOk;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task DeleteAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM Despesa ");
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

        public async Task<IEnumerable<Despesa>> GetAllAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Despesa.Id, DataMovimento, ValorPago, ");
            sb.Append("NumeroDocumento, IdTipoDespesa, IdCategoriaDespesa ");
            sb.Append("FROM Despesa");
            using (var connection = _context.CreateConnection())
            {
                var expenses = await connection.QueryAsync<Despesa>(sb.ToString());
                if (expenses != null)
                {
                    return expenses;
                }
                else
                {
                    return Enumerable.Empty<Despesa>();
                }
            }
        }
        public async Task<Despesa> GetByIdAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Despesa.Id, DataMovimento, ValorPago, ");
            sb.Append("NumeroDocumento, IdTipoDespesa, IdCategoriaDespesa ");
            sb.Append("FROM Despesa ");
            sb.Append($"WHERE Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var expense = await connection.QuerySingleOrDefaultAsync<Despesa>(sb.ToString(), new { Id });
                    if (expense != null)
                    {
                        return expense;
                    }
                    else
                    {
                        return new Despesa();
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex);
                throw;
            }
        }

        public List<DespesaVM> Query_ByYear(string sAno)
        {
            throw new NotImplementedException();
        }

        public decimal TotalDespesas(int iTipoDespesa = 0)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TipoDespesa>> GetTipoDespesa_ByCategoriaDespesa(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                using (var connection = _context.CreateConnection())
                {
                    return await connection.QueryAsync<TipoDespesa>("usp_Despesas_GetTipoDespesaByCategory",
                     param: parameters, commandType: CommandType.StoredProcedure);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<DespesaVM> GetVMByIdAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Despesa.Id, DataMovimento, ValorPago, ");
            sb.Append("NumeroDocumento, IdTipoDespesa, IdCategoriaDespesa, ");
            sb.Append("CD.Descricao AS [DescricaoCategoriaDespesa], ");
            sb.Append("TD.Descricao AS [DescricaoTipoDespesa], ");
            sb.Append("FROM Despesa ");
            sb.Append("INNER JOIN CategoriaDespesa CD ON ");
            sb.Append("Despesa.IdCategoriaDespesa = CD.Id ");
            sb.Append("INNER JOIN TipoCategoria TC ON ");
            sb.Append("Despesa.IdCategoriaDespesa = CD.Id ");
            sb.Append("WHERE Despesa.Id = @Id");

            using (var connection = _context.CreateConnection())
            {
                var expenseVM = await connection.QueryFirstOrDefaultAsync<DespesaVM>(sb.ToString(), new { Id });
                if (expenseVM != null)
                {
                    return expenseVM;
                }
                else
                {
                    return new DespesaVM();
                }
            }

        }

        public async Task<IEnumerable<DespesaVM>> GetAllVMAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Despesa.Id, DataMovimento, ValorPago, ");
            sb.Append("NumeroDocumento, IdTipoDespesa, IdCategoriaDespesa, ");
            sb.Append("CD.Descricao AS [DescricaoCategoriaDespesa], ");
            sb.Append("TD.Descricao AS [DescricaoTipoDespesa], ");
            sb.Append("FROM Despesa ");
            sb.Append("INNER JOIN CategoriaDespesa CD ON ");
            sb.Append("Despesa.IdCategoriaDespesa = CD.Id ");
            sb.Append("INNER JOIN TipoCategoria TC ON ");
            sb.Append("Despesa.IdCategoriaDespesa = CD.Id ");

            using (var connection = _context.CreateConnection())
            {
                var expensesVM = await connection.QueryAsync<DespesaVM>(sb.ToString());
                if (expensesVM != null)
                {
                    return expensesVM;
                }
                else
                {
                    return Enumerable.Empty<DespesaVM>();
                }
            }
        }
    }
}
