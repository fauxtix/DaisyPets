using Dapper;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Text;

namespace MauiPetsApp.Infrastructure.Repositories.Logs
{
    public class SQLiteSink : ILogEventSink
    {
        private readonly IDapperContext _context;
        private readonly IFormatProvider _formatProvider;
        public SQLiteSink(IDapperContext context, IFormatProvider formatProvider)
        {
            _context = context;
            EnsureDatabase().Wait();
            _formatProvider = formatProvider;
        }

        // Ensure the database and the PetsLogs table exist
        private async Task EnsureDatabase()
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {

                    StringBuilder sb = new StringBuilder();
                    sb.Append("CREATE TABLE IF NOT EXISTS PetsLogs (");
                    sb.Append("Id INTEGER PRIMARY KEY AUTOINCREMENT, ");
                    sb.Append("Message TEXT, ");
                    sb.Append("MessageTemplate TEXT, ");
                    sb.Append("Level TEXT, ");
                    sb.Append("TimeStamp TEXT, ");
                    sb.Append("Exception TEXT, ");
                    sb.Append("Properties TEXT)");
                    await connection.ExecuteAsync(sb.ToString());
                }

            }
            catch (Exception ex)
            {
                Log.Error($"Erro ao criar tabela de logs {ex.Message}");
            }
        }

        public async void Emit(LogEvent logEvent)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    StringBuilder sb = new();
                    sb.Append("INSERT INTO PetsLogs(Message, MessageTemplate, Level, TimeStamp, Exception, Properties) ");
                    sb.Append("VALUES(@Message, @MessageTemplate, @Level, @TimeStamp, @Exception, @Properties)");

                    DynamicParameters dynamicParameters = new DynamicParameters();

                    dynamicParameters.Add("@Message", logEvent.RenderMessage(_formatProvider));
                    dynamicParameters.Add("@MessageTemplate", logEvent.MessageTemplate.Text);
                    dynamicParameters.Add("@Level", logEvent.Level.ToString());
                    dynamicParameters.Add("@TimeStamp", logEvent.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
                    dynamicParameters.Add("@Exception", logEvent.Exception?.ToString());
                    dynamicParameters.Add("@Properties", logEvent.Properties.Count > 0 ? logEvent.Properties.ToString() : null);

                    await connection.ExecuteAsync(sb.ToString(), param: dynamicParameters);

                }
                catch (Exception ex)
                {
                    Log.Error($"Erro em SQLiteSink; {ex.Message}");

                }
            }
        }

    }
}
