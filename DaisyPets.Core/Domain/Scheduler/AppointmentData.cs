namespace DaisyPets.Core.Domain.Scheduler
{
    public class AppointmentData
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string? EndTime { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsAllDay { get; set; }
        public string RecurrenceRule { get; set; } = string.Empty;
        public string RecurrenceException { get; set; } = string.Empty;
        public Nullable<int> RecurrenceID { get; set; }
        public bool IsReadonly { get; set; } 
    }
}
