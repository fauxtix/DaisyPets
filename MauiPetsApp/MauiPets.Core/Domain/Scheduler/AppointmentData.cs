namespace MauiPetsApp.Core.Domain.Scheduler
{
    public class AppointmentData
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string? EndTime { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int IsAllDay { get; set; }
        public string RecurrenceRule { get; set; } = string.Empty;
        public string RecurrenceException { get; set; } = string.Empty;
        public int RecurrenceID { get; set; }
        public int IsReadonly { get; set; }
        public int AppointmentType { get; set; }
        public int PetId { get; set; }
    }
}
