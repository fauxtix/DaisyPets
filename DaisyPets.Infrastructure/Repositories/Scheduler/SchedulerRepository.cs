using DaisyPets.Core.Application.Interfaces.DapperContext;
using DaisyPets.Core.Application.Interfaces.Repositories.Scheduler;
using DaisyPets.Core.Application.TodoManager;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Scheduler;
using DaisyPets.Core.Domain.Scheduler;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DaisyPets.Infrastructure.Repositories.Scheduler
{
    public class SchedulerRepository : IScheduler
    {
        private readonly IDapperContext _context;
        private readonly ILogger<SchedulerRepository> _logger;

        public SchedulerRepository(IDapperContext context, ILogger<SchedulerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> InsertAsync(AppointmentData appointment)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO AppointmentData (");
            sb.Append("Subject, StartTime, EndTime, Location, IsAllDay, RecurrenceRule, RecurrenceException, RecurrenceId) ");
            sb.Append(" VALUES(");
            sb.Append("@Subject, @StartTime, @EndTime, @Location, @IsAllDay, @RecurrenceRule, @RecurrenceException, @RecurrenceId");
            sb.Append(");");
            sb.Append("SELECT last_insert_rowid()");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstAsync<int>(sb.ToString(), param: appointment);
                    return result;
                }

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
                return -1;
            }

        }


        public async Task UpdateAsync(int Id, AppointmentData appointment)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", appointment.Id);
            dynamicParameters.Add("@Subject", appointment.Subject);
            dynamicParameters.Add("@StartTime", appointment.StartTime);
            dynamicParameters.Add("@EndTime", appointment.EndTime);
            dynamicParameters.Add("@Location", appointment.Location);
            dynamicParameters.Add("@IsAllDay", appointment.IsAllDay);
            dynamicParameters.Add("@RecurrenceRule", appointment.RecurrenceRule);
            dynamicParameters.Add("@RecurrenceID", appointment.RecurrenceID);

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE AppointmentData SET ");
            sb.Append("Subject = @Subject, ");
            sb.Append("StartTime = @Motivo, ");
            sb.Append("EndTime = @EndTime, ");
            sb.Append("Location = @Location, ");
            sb.Append("IsAllDay = @IsAllDay, ");
            sb.Append("RecurrenceRule = @RecurrenceRule ");
            sb.Append("RecurrenceID = @RecurrenceID ");
            sb.Append("WHERE Id = @Id");

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sb.ToString(), param: dynamicParameters);
            }

        }

        public async Task DeleteAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM AppointmentData ");
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

        public async Task<AppointmentData> FindByIdAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM AppointmentData ");
            sb.Append($"WHERE Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var pet = await connection.QuerySingleOrDefaultAsync<AppointmentData>(sb.ToString(), new { Id });
                    if (pet != null)
                    {
                        return pet;
                    }
                    else
                    {
                        return new AppointmentData();
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex);
                throw;
            }
        }

        public async Task<IEnumerable<AppointmentData>> GetAllAsync()
        {
            List<AppointmentData> apptsList = new List<AppointmentData>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM AppointmentData ");
            using (var connection = _context.CreateConnection())
            {
                var Appointments = await connection.QueryAsync<AppointmentData>(sb.ToString());
                if (Appointments.Any())
                {
                    return Appointments;
                }
                else
                {
                    var vaccinesAppts = await GetVaccinesAppts();
                    if (vaccinesAppts.Any())
                    {
                        apptsList.AddRange(vaccinesAppts);
                    }
                    var dewordmersAppts = await GetDewormersAppts();
                    if (dewordmersAppts.Any())
                    {
                        apptsList.AddRange(dewordmersAppts);
                    }
                    var veterinaryAppts = await GetVeterinaryAppts();
                    if (veterinaryAppts.Any())
                    {
                        apptsList.AddRange(veterinaryAppts);
                    }

                    var todoListAppts = await GetTodoList();
                    if (todoListAppts.Any())
                    {
                        apptsList.AddRange(todoListAppts);
                    }

                    if (apptsList.Count > 0)
                    {
                        return apptsList;
                    }

                    return Enumerable.Empty<AppointmentData>();
                }
            }
        }

        public async Task<IEnumerable<AppointmentDataDto>> GetAllVMAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppointmentDataDto>> GetAppointmentVMAsync(int Id)
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<AppointmentData>> GetVaccinesAppts()
        {
            List<AppointmentData> vaccinesList = new List<AppointmentData>();

            var readOnly = true;

            StringBuilder sbVaccines = new StringBuilder();
            sbVaccines.Append("SELECT Vacina.Id, Vacina.IdPet, Vacina.DataToma, Vacina.Marca, ");
            sbVaccines.Append("Vacina.ProximaTomaEmMeses, Vacina.ProximaTomaEmMeses, ");
            sbVaccines.Append("Pet.Nome AS [NomePet] ");
            sbVaccines.Append("FROM Vacina ");
            sbVaccines.Append("INNER JOIN Pet ON ");
            sbVaccines.Append("Vacina.IdPet = Pet.Id");

            using (var connection = _context.CreateConnection())
            {
                var vaccines = (await connection.QueryAsync<VacinaVM>(sbVaccines.ToString())).ToList();
                if (vaccines.Any())
                {
                    foreach (var vaccine in vaccines)
                    {
                        var vaccineNextApplication = DateTime.Parse(vaccine.DataToma).AddMonths(vaccine.ProximaTomaEmMeses).ToShortDateString();
                        //var startTime = DateTime.Parse(vaccine.DataToma).Date;
                        //if (startTime < DateTime.Now.Date)
                        //{
                        //    readOnly = true;
                        //}

                        vaccinesList.Add(new AppointmentData
                        {
                            Id = vaccine.Id,
                            StartTime = vaccine.DataToma,
                            EndTime = vaccine.DataToma,
                            Subject = "Toma de vacina",
                            IsAllDay = true,
                            Location = "Veterinário(a)",
                            IsReadonly = readOnly,
                            Description = $"<b>{vaccine.NomePet}</b> - Toma da vacina {vaccine.Marca}",
                        });

                        var endTime = DateTime.Parse(vaccine.DataToma).Date.AddMonths(vaccine.ProximaTomaEmMeses);
                        if (endTime < DateTime.Now.Date)
                        {
                            readOnly = true;
                        }

                        vaccinesList.Add(new AppointmentData
                        {
                            Id = vaccine.Id,
                            StartTime = vaccineNextApplication,
                            EndTime = vaccineNextApplication,
                            Subject = "Toma de vacina",
                            IsAllDay = true,
                            IsReadonly = readOnly,
                            Location = "Veterinário(a)",
                            Description = $"{vaccine.NomePet} - Toma da vacina {vaccine.Marca}",
                        });
                    }

                    return vaccinesList;
                }
                return new List<AppointmentData>();
            }
        }

        private async Task<IEnumerable<AppointmentData>> GetDewormersAppts()
        {
            List<AppointmentData> dewormersList = new List<AppointmentData>();
            StringBuilder sbDewordmers = new StringBuilder();

            sbDewordmers.Append("SELECT Desparasitante.Id, DataAplicacao, DataProximaAplicacao, ");
            sbDewordmers.Append("Marca, Tipo, IdPet, Pet.Nome AS [NomePet] ");
            sbDewordmers.Append("FROM Desparasitante ");
            sbDewordmers.Append("INNER JOIN Pet ON ");
            sbDewordmers.Append("Desparasitante.IdPet = Pet.Id ");

            var readOnly = true;
            using (var connection = _context.CreateConnection())
            {
                var dewormers = (await connection.QueryAsync<DesparasitanteVM>(sbDewordmers.ToString())).ToList();
                if (dewormers.Any())
                {
                    foreach (var dewormer in dewormers)
                    {
                        //var startTime = DateTime.Parse(dewormer.DataAplicacao).Date;
                        //if (startTime < DateTime.Now.Date)
                        //{
                        //    readOnly = true;
                        //}

                        dewormersList.Add(new AppointmentData
                        {
                            Id = dewormer.Id,
                            StartTime = dewormer.DataProximaAplicacao,
                            EndTime = dewormer.DataProximaAplicacao,
                            Subject = "Toma de desparasitante",
                            IsAllDay = true,
                            IsReadonly = readOnly,
                            Location = "Home application",
                            Description = $"{dewormer.NomePet} - {dewormer.Marca}"
                        });
                    }
                    return dewormersList;
                }
                { return new List<AppointmentData>(); }
            }
        }

        private async Task<IEnumerable<AppointmentData>> GetVeterinaryAppts()
        {
            List<AppointmentData> veterinaryApptsList = new List<AppointmentData>();
            StringBuilder sbAppts = new StringBuilder();

            sbAppts.Append("SELECT ConsultaVeterinario.Id, DataConsulta, Motivo, ");
            sbAppts.Append("Diagnostico, Tratamento, IdPet, Pet.Nome AS [NomePet] ");
            sbAppts.Append("FROM ConsultaVeterinario ");
            sbAppts.Append("INNER JOIN Pet ON ");
            sbAppts.Append("ConsultaVeterinario.IdPet = Pet.Id");

            var readOnly = true;
            using (var connection = _context.CreateConnection())
            {
                var appts = (await connection.QueryAsync<ConsultaVeterinarioVM>(sbAppts.ToString())).ToList();
                if (appts.Any())
                {
                    foreach (var appt in appts)
                    {
                        //var startTime = DateTime.Parse(appt.DataConsulta).Date;
                        //if (startTime < DateTime.Now.Date)
                        //{
                        //    readOnly = true;
                        //}

                        veterinaryApptsList.Add(new AppointmentData
                        {
                            Id = appt.Id,
                            StartTime = appt.DataConsulta,
                            EndTime = appt.DataConsulta,
                            Subject = "Consulta no veterinário",
                            IsAllDay = true,
                            IsReadonly = readOnly,
                            Location = "VetMonti",
                            Description = $"{appt.NomePet} - Motivo: {appt.Motivo} - Tratamento: {appt.Tratamento}"
                        });
                    }
                    return veterinaryApptsList;
                }

                { return new List<AppointmentData>(); }
            }
        }

        private async Task<IEnumerable<AppointmentData>> GetTodoList()
        {
            List<AppointmentData> todoList = new List<AppointmentData>();
            StringBuilder sbTodoLists= new StringBuilder();

            sbTodoLists.Append("SELECT ToDo.Id, ToDo.Description, StartDate, EndDate, Status, Generated, ");
            sbTodoLists.Append("TodoCategories.Id as [CategoryId], TodoCategories.Descricao AS [CategoryDescription] ");
            sbTodoLists.Append("FROM ToDo ");
            sbTodoLists.Append("INNER JOIN ToDoCategories ON ");
            sbTodoLists.Append("ToDo.CategoryId = ToDoCategories.Id");

            using (var connection = _context.CreateConnection())
            {
                var appts = (await connection.QueryAsync<ToDoDto>(sbTodoLists.ToString())).ToList();
                if (appts.Any())
                {
                    foreach (var appt in appts)
                    {
                        todoList.Add(new AppointmentData
                        {
                            Id = appt.Id,
                            StartTime = appt.StartDate,
                            EndTime = appt.StartDate,
                            Subject = "Todo event",
                            IsAllDay = true,
                            IsReadonly = true,
                            Location = appt.CategoryDescription,
                            Description = appt.Description
                        });
                    }
                    return todoList;
                }

                { return new List<AppointmentData>(); }
            }
        }

    }
}

