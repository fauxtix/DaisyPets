﻿using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain.Blog;

namespace DaisyPets.Core.Application.Interfaces.Repositories.Blog
{
    public interface IBlogRepository
    {
        Task DeletePostAsync(int Id);
        Task<Post> FindPostByIdAsync(int Id);
        Task<IEnumerable<Post>> GetAllAsync();
        Task<IEnumerable<PostDto>> GetAllPostsVMAsync();
        Task<IEnumerable<Comment>> GetPostComments(int Id);
        Task<IEnumerable<PostDto>> GetPostVMAsync(int Id);
        Task<int> InserPostAsync(Post post);
        Task<int> InserPostCommentAsync(Comment comment);
        Task UpdatePostAsync(int Id, Post post);
    }
}