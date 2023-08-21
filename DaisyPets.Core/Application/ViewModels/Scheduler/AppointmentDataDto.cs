namespace DaisyPets.Core.Application.ViewModels.Scheduler
{
    public class AppointmentDataDto
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsAllDay { get; set; }
        public string RecurrenceRule { get; set; } = string.Empty;
        public string RecurrenceException { get; set; } = string.Empty;
        public Nullable<int> RecurrenceID { get; set; }
        public bool IsReadonly { get; set; } 
    }
}
