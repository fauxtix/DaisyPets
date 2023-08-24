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
            var sbInsert = new StringBuilder();
            var insertParams = new DynamicParameters();

            insertParams.Add("@Subject", appointment.Subject);
            insertParams.Add("@Location", appointment.Location);
            insertParams.Add("@StartTime", appointment.StartTime);
            insertParams.Add("@EndTime", appointment.EndTime);
            insertParams.Add("@Description", appointment.Description);
            insertParams.Add("@IsAllDay", appointment.IsAllDay);
            insertParams.Add("@IsReadonly", appointment.IsReadonly);
            insertParams.Add("@RecurrenceRule", appointment.RecurrenceRule);
            insertParams.Add("@RecurrenceException", appointment.RecurrenceException);
            insertParams.Add("@RecurrenceId", appointment.RecurrenceID);
            insertParams.Add("@AppointmentType", appointment.AppointmentType);

            sbInsert.Append("INSERT INTO AppointmentData (");
            sbInsert.Append("Subject, Location, StartTime, EndTime,  ");
            sbInsert.Append("Description, IsAllDay, IsReadonly, RecurrenceRule, ");
            sbInsert.Append("RecurrenceException, RecurrenceId, AppointmentType) ");

            sbInsert.Append("VALUES(");
            sbInsert.Append("@Subject, @Location, @StartTime, @EndTime, ");
            sbInsert.Append("@Description, @IsAllDay, @IsReadonly, @RecurrenceRule, ");
            sbInsert.Append("@RecurrenceException, @RecurrenceId, @AppointmentType) ");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(sbInsert.ToString(), insertParams);

                    sbInsert.Clear();
                    sbInsert.Append("SELECT last_insert_rowid()");

                    var pkId = await connection.QuerySingleOrDefaultAsync<int>(sbInsert.ToString());
                    return pkId;
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
            DynamicParameters updateParams = new DynamicParameters();
            updateParams.Add("@Id", appointment.Id);
            updateParams.Add("@Subject", appointment.Subject);
            updateParams.Add("@Location", appointment.Location);
            updateParams.Add("@StartTime", appointment.StartTime);
            updateParams.Add("@EndTime", appointment.EndTime);
            updateParams.Add("@Description", appointment.Description);
            updateParams.Add("@IsAllDay", appointment.IsAllDay);
            updateParams.Add("@IsReadonly", appointment.IsReadonly);
            updateParams.Add("@RecurrenceRule", appointment.RecurrenceRule ?? "");
            updateParams.Add("@RecurrenceException", appointment.RecurrenceException ?? "");
            updateParams.Add("@RecurrenceID", appointment.RecurrenceID);
            updateParams.Add("@AppointmentType", appointment.AppointmentType);



            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE AppointmentData SET ");
            sb.Append("Subject = @Subject, ");
            sb.Append("Location = @Location, ");
            sb.Append("StartTime = @StartTime, ");
            sb.Append("EndTime = @EndTime, ");
            sb.Append("Description = @Description, ");
            sb.Append("IsAllDay = @IsAllDay, ");
            sb.Append("IsReadonly = @IsReadonly, ");
            sb.Append("RecurrenceRule = @RecurrenceRule, ");
            sb.Append("RecurrenceException = @RecurrenceException, ");
            sb.Append("RecurrenceID = @RecurrenceID, ");
            sb.Append("AppointmentType = @AppointmentType ");
            sb.Append("WHERE Id = @Id");

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sb.ToString(), updateParams);
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
                var standardAppointments = await connection.QueryAsync<AppointmentData>(sb.ToString());
                if (standardAppointments.Any())
                {
                    apptsList.AddRange(standardAppointments);
                }

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

            var readOnly = 1;

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
                            IsAllDay = 1,
                            Location = "Veterinário(a)",
                            IsReadonly = readOnly,
                            RecurrenceException = "",
                            RecurrenceID = 0,
                            RecurrenceRule = "",
                            Description = $"<b>{vaccine.NomePet}</b> - Toma da vacina {vaccine.Marca}",
                            AppointmentType = 1
                        });

                        var endTime = DateTime.Parse(vaccine.DataToma).Date.AddMonths(vaccine.ProximaTomaEmMeses);
                        if (endTime < DateTime.Now.Date)
                        {
                            readOnly = 1;
                        }

                        vaccinesList.Add(new AppointmentData
                        {
                            Id = vaccine.Id,
                            StartTime = vaccineNextApplication,
                            EndTime = vaccineNextApplication,
                            Subject = "Toma de vacina",
                            IsAllDay = 1,
                            IsReadonly = readOnly,
                            Location = "Veterinário(a)",
                            RecurrenceException = "",
                            RecurrenceID = 0,
                            RecurrenceRule = "",
                            Description = $"{vaccine.NomePet} - Toma da vacina {vaccine.Marca}",
                            AppointmentType = 1
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
                            IsAllDay = 1,
                            IsReadonly = 1,
                            RecurrenceException = "",
                            RecurrenceID = 0,
                            RecurrenceRule = "",
                            Location = "Home application",
                            Description = $"{dewormer.NomePet} - {dewormer.Marca}",
                            AppointmentType = 2
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
                            IsAllDay = 1,
                            IsReadonly = 1,
                            RecurrenceException = "",
                            RecurrenceID = 0,
                            RecurrenceRule = "",
                            Location = "Veterinário",
                            Description = $"{appt.NomePet} - Motivo: {appt.Motivo} - Tratamento: {appt.Tratamento}",
                            AppointmentType = 3
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
            StringBuilder sbTodoLists = new StringBuilder();

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
                            IsAllDay = 1,
                            IsReadonly = 1,
                            RecurrenceException = "",
                            RecurrenceID = 0,
                            RecurrenceRule = "",
                            Location = appt.CategoryDescription,
                            Description = appt.Description,
                            AppointmentType = 4
                        });
                    }
                    return todoList;
                }

                { return new List<AppointmentData>(); }
            }
        }

    }
}

