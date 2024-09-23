using MauiPetsApp.Core.Application.Exceptions;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MauiPetsApp.Infrastructure.OldRepositories
{
    public class ContactRepository : IContactRepository
    {
        DataAccessStatus dataAccessStatus = new DataAccessStatus();
        private readonly IDapperContext _context;
        private readonly ILogger<ContactRepository> _logger;

        public ContactRepository(IDapperContext context, ILogger<ContactRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task DeleteAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM Contacto ");
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

        public async Task<Contacto> FindByIdAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Contacto ");
            sb.Append($"WHERE Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var pet = await connection.QuerySingleOrDefaultAsync<Contacto>(sb.ToString(), new { Id });
                    if (pet != null)
                    {
                        return pet;
                    }
                    else
                    {
                        return new Contacto();
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex);
                throw;
            }
        }

        public async Task<IEnumerable<Contacto>> GetAllAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Contacto ");
            using (var connection = _context.CreateConnection())
            {
                var contacts = await connection.QueryAsync<Contacto>(sb.ToString());
                if (contacts != null)
                {
                    return contacts;
                }
                else
                {
                    return Enumerable.Empty<Contacto>();
                }
            }
        }

        public async Task<IEnumerable<ContactoVM>> GetAllContactVMAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Contacto.Id, Nome, Morada, Localidade, Movel, EMail, ");
            sb.Append("Notas, IdTipoContacto, TipoContacto.Descricao AS [DescricaoTipoContacto] ");
            sb.Append("FROM Contacto ");
            sb.Append("INNER JOIN TipoContacto ON ");
            sb.Append("Contacto.IdTipoContacto = TipoContacto.Id");


            using (var connection = _context.CreateConnection())
            {
                var contactsVM = await connection.QueryAsync<ContactoVM>(sb.ToString());
                if (contactsVM != null)
                {
                    return contactsVM;
                }
                else
                {
                    return Enumerable.Empty<ContactoVM>();
                }
            }
        }

        public async Task<ContactoVM> GetContactVMAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Contacto.Id, Nome, Morada, Localidade, Movel, EMail, ");
            sb.Append("Notas, IdTipoContacto, TipoContacto.Descricao AS [DescricaoTipoContacto] ");
            sb.Append("FROM Contacto ");
            sb.Append("INNER JOIN TipoContacto ON ");
            sb.Append("Contacto.IdTipoContacto = TipoContacto.Id ");
            sb.Append("WHERE Contacto.Id = @Id");


            using (var connection = _context.CreateConnection())
            {
                var contactVM = await connection.QueryFirstOrDefaultAsync<ContactoVM>(sb.ToString(), new { Id });
                if (contactVM != null)
                {
                    return contactVM;
                }
                else
                {
                    return new ContactoVM();
                }
            }
        }

        public async Task<int> InsertAsync(Contacto contact)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO Contacto (");
            sb.Append("Nome, Morada, Localidade, Movel, eMail, Notas, IdTipoContacto) ");
            sb.Append(" VALUES(");
            sb.Append("@Nome, @Morada, @Localidade, @Movel, @eMail, @Notas,  @IdTipoContacto");
            sb.Append(");");
            sb.Append("SELECT last_insert_rowid()");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstAsync<int>(sb.ToString(), param: contact);
                    return result;
                }

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
                return -1;
            }

        }

        public async Task UpdateAsync(int Id, Contacto contact)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", contact.Id);
            dynamicParameters.Add("@Nome", contact.Nome);
            dynamicParameters.Add("@Morada", contact.Morada);
            dynamicParameters.Add("@Localidade", contact.Localidade);
            dynamicParameters.Add("@Movel", contact.Movel);
            dynamicParameters.Add("@eMail", contact.eMail);
            dynamicParameters.Add("@Notas", contact.Notas);
            dynamicParameters.Add("@IdTipoContacto", contact.IdTipoContacto);

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE Contacto SET ");
            sb.Append("Nome = @Nome, ");
            sb.Append("Morada = @Morada, ");
            sb.Append("Localidade = @Localidade, ");
            sb.Append("Movel = @Movel, ");
            sb.Append("eMail = @eMail, ");
            sb.Append("Notas = @Notas, ");
            sb.Append("IdTipoContacto = @IdTipoContacto ");
            sb.Append("WHERE Id = @Id");

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sb.ToString(), param: dynamicParameters);
            }

        }

        public async Task<IEnumerable<ContactoVM>> SearchContactByNamet(string filter)
        {
            var contacts = (await GetAllContactVMAsync())
                .ToList().
                Where(c => c.Nome.Contains(filter));
            return contacts;


        }

    }
}
