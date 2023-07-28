namespace DaisyPets.Core.Domain.TodoManager
{
    public class ToDo
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int Status { get; set; }
        public int? CategoryId { get; set; }
    }
}
