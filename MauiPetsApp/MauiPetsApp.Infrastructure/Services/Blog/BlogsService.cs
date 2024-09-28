using AutoMapper;
using FluentValidation;
using MauiPetsApp.Core.Application.Interfaces.Repositories.Blog;
using MauiPetsApp.Core.Application.Interfaces.Services.Blog;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain.Blog;
using Serilog;

namespace MauiPetsApp.Infrastructure.Services.Blog
{
    public class BlogsService : IBlogService
    {
        private readonly IBlogRepository _repository;
        private readonly IValidator<PostDto> _validator;

        private readonly IMapper _mapper;
        public BlogsService(IBlogRepository repository, IValidator<PostDto> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task DeletePostAsync(int Id)
        {
            await _repository.DeletePostAsync(Id);
        }

        public async Task<PostDto> FindPostByIdAsync(int Id)
        {
            var resp = await _repository.FindPostByIdAsync(Id);
            var output = _mapper.Map<PostDto>(resp);
            return output;
        }

        public async Task<IEnumerable<PostDto>> GetAllAsync()
        {
            var resp = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<PostDto>>(resp);
            return output;
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsVMAsync()
        {
            try
            {
                var postsVM = await _repository.GetAllPostsVMAsync();
                return postsVM;

            }
            catch (Exception ex)
            {
                Log.Error($"Erro: {ex.Message}");
                return Enumerable.Empty<PostDto>();
            }
        }

        public async Task<PostDto> GetPostAsync(int Id)
        {
            var post = await _repository.GetPostAsync(Id);
            var mappedPost = _mapper.Map<PostDto>(post);
            return mappedPost; ;
        }

        public async Task<IEnumerable<CommentDto>> GetPostComments(int Id)
        {
            var posts = await _repository.GetPostComments(Id);
            var mappedComments = _mapper.Map<IEnumerable<CommentDto>>(posts);
            return mappedComments; ;

        }

        public async Task<IEnumerable<PostDto>> GetPostVMAsync(int Id)
        {
            return await _repository.GetPostVMAsync(Id);
        }

        public async Task<int> InsertPostAsync(PostDto post)
        {
            var postIdentity = _mapper.Map<Post>(post);
            var insertedId = await _repository.InsertPostAsync(postIdentity);
            return insertedId;
        }

        public async Task<int> InsertPostCommentAsync(CommentDto comment)
        {
            var commentIdentity = _mapper.Map<Comment>(comment);
            var insertedId = await _repository.InsertPostCommentAsync(commentIdentity);
            return insertedId;
        }

        public async Task UpdatePostAsync(int Id, PostDto post)
        {
            try
            {
                var postEntity = await _repository.FindPostByIdAsync(Id);
                if (postEntity == null)
                    throw new KeyNotFoundException("Post not found");

                var mappedModel = _mapper.Map(post, postEntity);

                await _repository.UpdatePostAsync(Id, mappedModel);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "Erro no update do post");

            }
        }
    }
}
