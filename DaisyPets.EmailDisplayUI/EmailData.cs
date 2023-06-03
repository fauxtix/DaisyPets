namespace DaisyPets.EmailDisplayUI
{
    public class EmailData
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public bool IsImportant { get; set; }
        public string Body { get; set; } = string.Empty;
    }
}
