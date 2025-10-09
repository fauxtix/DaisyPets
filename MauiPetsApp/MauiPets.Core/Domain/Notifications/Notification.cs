namespace MauiPets.Core.Domain.Notifications
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime ScheduledFor { get; set; }
        public int NotificationTypeId { get; set; }
        public bool IsRead { get; set; }
        public int? RelatedItemId { get; set; }
        public NotificationType Type { get; set; } = new NotificationType();
    }
}
