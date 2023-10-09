using Dapper;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MauiPetsApp.Infrastructure.Repositories
{
    public class DocumentsRepository : IDocumentsRepository
    {
        private readonly IDapperContext _context;
        private readonly ILogger<ContactRepository> _logger;

        public DocumentsRepository(IDapperContext context, ILogger<ContactRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> InsertDocument(Documento newDocument)
        {

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Title", newDocument.Title);
            dynamicParameters.Add("@Description", newDocument.Description);
            dynamicParameters.Add("@DocumentPath", newDocument.DocumentPath);
            dynamicParameters.Add("@CreatedOn", newDocument.CreatedOn);
            dynamicParameters.Add("@PetId", newDocument.PetId);

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO Documento(");
            sb.Append("Title, Description, DocumentPath, CreatedOn, PetId) VALUES (");
            sb.Append($"@Title, @Description, @DocumentPath, @CreatedOn, @PetId); ");
            sb.Append("SELECT last_insert_rowid()");

            try
            {
                newDocument.CreatedOn = DateTime.Now.ToShortDateString();
                using (var connection = _context.CreateConnection())
                {
                    int insertedId = await connection.QueryFirstAsync<int>(sb.ToString(), param: dynamicParameters);
                    return insertedId;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return -1;
            }
        }

        public async Task<bool> UpdateDocument(Documento updateDocument)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE Documento SET ");
            sb.Append("Title = @Title, ");
            sb.Append("Description = @Description, ");
            sb.Append("DocumentPath = @DocumentPath, ");
            sb.Append("PetId = @PetId ");
            sb.Append("WHERE Id = @Id");

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", updateDocument.Id);
            dynamicParameters.Add("@Title", updateDocument.Title);
            dynamicParameters.Add("@Description", updateDocument.Description);
            dynamicParameters.Add("@DocumentPath", updateDocument.DocumentPath);
            dynamicParameters.Add("@PetId", updateDocument.PetId);

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.ExecuteAsync(sb.ToString(),
                         param: dynamicParameters);

                    return result > 0;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task DeleteDocument(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM Documento ");
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
                _logger.LogError(ex.Message);
            }
        }


        public async Task<IEnumerable<Documento>> GetAll()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Id, Title, Description, DocumentPath, CreatedOn, PetId ");
            sb.Append("FROM Documento");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var output = await connection.QueryAsync<Documento>(sb.ToString());
                    return output.ToList();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null!;
            }
        }

        public async Task<Documento> GetDocument_ById(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Id, Title, Description, DocumentPath, CreatedOn, PetId ");
            sb.Append("FROM Documento ");
            sb.Append("WHERE Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var output = await connection.QueryFirstOrDefaultAsync<Documento>(sb.ToString(),
                        param: new { Id = id });
                    return output;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null!;
            }
        }


        public async Task<IEnumerable<DocumentoVM>> GetAllVM(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT D.Id, D.Title, D.Description, D.DocumentPath, D.CreatedOn, D.PetId, P.Nome AS [PetName] ");
            sb.Append("FROM Documento D ");
            sb.Append("INNER JOIN Pet P ON ");
            sb.Append("D.PetId = P.Id ");
            sb.Append("WHERE D.PetId = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var output = await connection.QueryAsync<DocumentoVM>(sb.ToString(), new { Id });
                    return output;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null!;
            }
        }
    }
}
