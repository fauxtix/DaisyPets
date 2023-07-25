namespace DaisyPets.Core.Application.ViewModels
{
    public class PostDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Introduction { get; set; }
        public string? BodyText { get; set; }
        public string? Image { get; set; }
        public ICollection<CommentDto>? Comments { get; set; }
    }
}