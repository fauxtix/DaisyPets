using AutoMapper;
using DaisyPets.Core.Application.Interfaces.Repositories.Blog;
using DaisyPets.Core.Application.Interfaces.Services.Blog;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain.Blog;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DaisyPets.Infrastructure.Services.Blog
{
    public class BlogsService : IBlogService
    {
        private readonly IBlogRepository _repository;
        private readonly IValidator<PostDto> _validator;

        private readonly IMapper _mapper;
        private readonly ILogger<BlogsService> _logger;

        public BlogsService(IBlogRepository repository, IValidator<PostDto> validator, IMapper mapper, ILogger<BlogsService> logger)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
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
                _logger.LogError($"Erro: {ex.Message}");
                throw;
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
                _logger.LogError(ex.Message, "Erro no update do post");
                throw;
            }
        }
    }
}
