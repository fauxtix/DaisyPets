using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain.Blog;

namespace DaisyPets.Core.Application.Interfaces.Services.Blog
{
    public interface IBlogService
    {
        Task DeletePostAsync(int Id);
        Task<PostDto> FindPostByIdAsync(int Id);
        Task<IEnumerable<PostDto>> GetAllAsync();
        Task<IEnumerable<PostDto>> GetAlPostsVMAsync();
        Task<IEnumerable<PostDto>> GetPostVMAsync(int Id);
        Task<int> InserPostAsync(PostDto post);
        Task<int> InserPostCommentAsync(CommentDto comment);
        Task UpdatePostAsync(int Id, PostDto post);

    }
}
