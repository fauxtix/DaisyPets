namespace DaisyPets.Core.Application.TodoManager
{
    public class ToDoDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int Status { get; set; } = 1; // pending
        public int? CategoryId { get; set; }
        public string? CategoryDescription { get; set; }
    }
}
