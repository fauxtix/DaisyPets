namespace DaisyPets.Core.Domain.Blog
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string? BlogUserId { get; set; }
        public string? CommentText { get; set; }
        public DateTime Created { get; set; }
    }
}
