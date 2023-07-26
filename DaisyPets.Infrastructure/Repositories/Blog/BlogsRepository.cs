using DaisyPets.Core.Application.Interfaces.DapperContext;
using DaisyPets.Core.Application.Interfaces.Repositories.Blog;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain.Blog;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DaisyPets.Infrastructure.Repositories.Blog
{
    public class BlogsRepository : IBlogRepository
    {
        private readonly IDapperContext _context;
        private readonly ILogger<ConsultaRepository> _logger;

        public BlogsRepository(IDapperContext context, ILogger<ConsultaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> InserPostAsync(Post post)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO Post (");
            sb.Append("Title, Introduction, BodyText, Image) ");
            sb.Append(" VALUES(");
            sb.Append("@Title, @Introduction, @BodyText, @Image");
            sb.Append(");");
            sb.Append("SELECT last_insert_rowid()");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstAsync<int>(sb.ToString(), param: post);
                    return result;
                }

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
                return -1;
            }
        }

        public async Task UpdatePostAsync(int Id, Post post)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", post.Id);
            dynamicParameters.Add("@Title", post.Title);
            dynamicParameters.Add("@Introduction", post.Introduction);
            dynamicParameters.Add("@BodyText", post.BodyText);
            dynamicParameters.Add("@Image", post.Image);

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE Post SET ");
            sb.Append("Title = @Title, ");
            sb.Append("Introduction = @Introduction, ");
            sb.Append("BodyText = @BodyText, ");
            sb.Append("Image = @Image, ");
            sb.Append("WHERE Id = @Id");

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sb.ToString(), param: dynamicParameters);
            }
        }

        public async Task DeletePostAsync(int Id)
        {
            // TODO - see if post can be deleted (childs)
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM Post ");
            sb.Append("WHERE Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(sb.ToString(), new { Id });
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
            }

        }

        public async Task<Post> FindPostByIdAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Post.Id, Title, Introduction, BodyText, Image ");
            sb.Append("FROM Post ");
            sb.Append($"WHERE Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var pet = await connection.QuerySingleOrDefaultAsync<Post>(sb.ToString(), new { Id });
                    if (pet != null)
                    {
                        return pet;
                    }
                    else
                    {
                        return new Post();
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex);
                throw;
            }
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Post ");
            using (var connection = _context.CreateConnection())
            {
                var posts = await connection.QueryAsync<Post>(sb.ToString());
                if (posts != null)
                {
                    return posts;
                }
                else
                {
                    return Enumerable.Empty<Post>();
                }
            }
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsVMAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  Post.Id, Title, Introduction, ");
            sb.Append("BodyText, Image ");
            //sb.Append("Comment.Commenttext ");
            sb.Append("FROM Post ");
            //sb.Append("INNER JOIN Comment ON ");
            //sb.Append("Comment.PostId = Post.Id");


            using (var connection = _context.CreateConnection())
            {
                var PostsVM = await connection.QueryAsync<PostDto>(sb.ToString());
                if (PostsVM != null)
                {
                    return PostsVM;
                }
                else
                {
                    return Enumerable.Empty<PostDto>();
                }
            }
        }

        public async Task<IEnumerable<PostDto>> GetPostVMAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  Post.Id, Title, Introduction, ");
            sb.Append("BodyText, Image, Comment.Commenttext ");
            sb.Append("FROM Post ");
            sb.Append("INNER JOIN Comment ON ");
            sb.Append("Comment.PostId = Post.Id");
            sb.Append("WHERE Post.Id = @Id");


            using (var connection = _context.CreateConnection())
            {
                var PostVM = await connection.QueryAsync<PostDto>(sb.ToString(), new { Id });
                if (PostVM != null)
                {
                    return PostVM;
                }
                else
                {
                    return Enumerable.Empty<PostDto>();
                }
            }
        }

        public async Task<int> InserPostCommentAsync(Comment comment)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@PostId", comment.PostId);
            dynamicParameters.Add("@BlogUserId", comment.BlogUserId);
            dynamicParameters.Add("@CommentText", comment.CommentText);
            dynamicParameters.Add("@Created", DateTime.UtcNow.ToShortDateString());

            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO Comment (");
            sb.Append("PostId, BlogUserId, CommentText, Created) ");
            sb.Append(" VALUES(");
            sb.Append("@PostId, @BlogUserId, @CommentText, @Created");
            sb.Append(");");
            sb.Append("SELECT last_insert_rowid()");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstAsync<int>(sb.ToString(), param: comment);
                    return result;
                }

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
                return -1;
            }

        }

        public async Task<IEnumerable<Comment>> GetPostComments(int Id)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM Comment ");
            sb.Append("WHERE PostId = @Id");
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<Comment>(sb.ToString(), param: new { Id });
                    return result;
                }

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
                return Enumerable.Empty<Comment>();
            }

        }
    }
}
