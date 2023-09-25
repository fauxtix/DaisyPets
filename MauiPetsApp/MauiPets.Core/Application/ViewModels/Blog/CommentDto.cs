namespace MauiPetsApp.Core.Application.ViewModels
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string? BlogUserId { get; set; }
        public string? CommentText { get; set; }
        public DateTime Created { get; set; }

        public BlogUser? BlogUser { get; set; }
        public PostDto? Post { get; set; }
    }
}
