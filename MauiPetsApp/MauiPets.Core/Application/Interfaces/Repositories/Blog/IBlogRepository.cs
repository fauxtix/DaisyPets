using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain.Blog;

namespace MauiPetsApp.Core.Application.Interfaces.Repositories.Blog
{
    public interface IBlogRepository
    {
        Task DeletePostAsync(int Id);
        Task<Post> FindPostByIdAsync(int Id);
        Task<IEnumerable<Post>> GetAllAsync();
        Task<IEnumerable<PostDto>> GetAllPostsVMAsync();
        Task<PostDto> GetPostAsync(int Id);
        Task<IEnumerable<Comment>> GetPostComments(int Id);
        Task<IEnumerable<PostDto>> GetPostVMAsync(int Id);
        Task<int> InsertPostAsync(Post post);
        Task<int> InsertPostCommentAsync(Comment comment);
        Task UpdatePostAsync(int Id, Post post);
    }
}