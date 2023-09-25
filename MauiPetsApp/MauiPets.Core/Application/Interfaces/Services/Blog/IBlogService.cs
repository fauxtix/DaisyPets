using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain.Blog;

namespace MauiPetsApp.Core.Application.Interfaces.Services.Blog
{
    public interface IBlogService
    {
        Task DeletePostAsync(int Id);
        Task<PostDto> FindPostByIdAsync(int Id);
        Task<IEnumerable<PostDto>> GetAllAsync();
        Task<IEnumerable<PostDto>> GetAllPostsVMAsync();
        Task<PostDto> GetPostAsync(int Id);
        Task<IEnumerable<PostDto>> GetPostVMAsync(int Id);
        Task<IEnumerable<CommentDto>> GetPostComments(int Id);
        Task<int> InsertPostAsync(PostDto post);
        Task<int> InsertPostCommentAsync(CommentDto comment);
        Task UpdatePostAsync(int Id, PostDto post);

    }
}
