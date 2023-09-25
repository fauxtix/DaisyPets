namespace MauiPetsApp.Core.Application.ViewModels
{
    public class PostDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Introduction { get; set; }
        public string? BodyText { get; set; }
        public string? Image { get; set; }
        public string? PostUrl { get; set; } = string.Empty;
        //public ICollection<CommentDto>? Comments { get; set; }
    }
}