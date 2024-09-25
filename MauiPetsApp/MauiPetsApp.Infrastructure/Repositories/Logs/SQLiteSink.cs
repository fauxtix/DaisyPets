using Dapper;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using Serilog.Core;
using Serilog.Events;

namespace MauiPetsApp.Infrastructure.Repositories.Logs
{
    public class SQLiteSink : ILogEventSink
    {
        private readonly IDapperContext _context;
        private readonly IFormatProvider _formatProvider;
        public SQLiteSink(IDapperContext context, IFormatProvider formatProvider)
        {
            _context = context;
            EnsureDatabase();
            _formatProvider = formatProvider;
        }

        // Ensure the database and the PetsLogs table exist
        private void EnsureDatabase()
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS PetsLogs (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Message TEXT NOT NULL,
                    MessageTemplate TEXT,
                    Level TEXT,
                    TimeStamp TEXT,
                    Exception TEXT,
                    Properties TEXT
                );";
                command.ExecuteNonQuery();
            }
        }

        public void Emit(LogEvent logEvent)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                INSERT INTO PetsLogs (Message, MessageTemplate, Level, TimeStamp, Exception, Properties) 
                VALUES (@Message, @MessageTemplate, @Level, @TimeStamp, @Exception, @Properties)";

                DynamicParameters dynamicParameters = new DynamicParameters();

                dynamicParameters.Add("@Message", logEvent.RenderMessage(_formatProvider));
                dynamicParameters.Add("@MessageTemplate", logEvent.MessageTemplate.Text);
                dynamicParameters.Add("@Level", logEvent.Level.ToString());
                dynamicParameters.Add("@TimeStamp", logEvent.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
                dynamicParameters.Add("@Exception", logEvent.Exception?.ToString());
                dynamicParameters.Add("@Properties", logEvent.Properties.Count > 0 ? logEvent.Properties.ToString() : null);

                command.ExecuteNonQuery();
            }
        }

    }
}
