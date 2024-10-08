namespace MauiPets.Core.Application.ViewModels.Logs
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; } = string.Empty;
        public string Properties { get; set; } = string.Empty;
    }
}
