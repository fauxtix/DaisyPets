﻿using Dapper;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using MauiPetsApp.Core.Application.Interfaces.Repositories.Blog;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain.Blog;
using Serilog;
using System.Text;

namespace MauiPetsApp.Infrastructure.Blog
{
    public class BlogsRepository : IBlogRepository
    {
        private readonly IDapperContext _context;

        public BlogsRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<int> InsertPostAsync(Post post)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO Post (");
            sb.Append("Title, Introduction, BodyText, Image, PostUrl) ");
            sb.Append(" VALUES(");
            sb.Append("@Title, @Introduction, @BodyText, @Image, @PostUrl");
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
                Log.Error(ex.ToString());
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
            dynamicParameters.Add("@PostUrl", post.PostUrl);

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE Post SET ");
            sb.Append("Title = @Title, ");
            sb.Append("Introduction = @Introduction, ");
            sb.Append("BodyText = @BodyText, ");
            sb.Append("Image = @Image, ");
            sb.Append("PostUrl = @PostUrl ");
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
                Log.Error(ex.ToString());
            }

        }

        public async Task<Post> FindPostByIdAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Post.Id, Title, Introduction, BodyText, Image, PostUrl ");
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
                Log.Error(ex.ToString(), ex);
                return new Post();
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
            sb.Append("BodyText, Image, PostUrl ");
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
            sb.Append("BodyText, Image, PostUrl, Comment.Commenttext ");
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

        public async Task<PostDto> GetPostAsync(int Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Id, Title, Introduction, ");
            sb.Append("BodyText, Image, PostUrl ");
            sb.Append("FROM Post ");
            sb.Append("WHERE Id = @Id");


            using (var connection = _context.CreateConnection())
            {
                var post = await connection.QueryFirstOrDefaultAsync<PostDto>(sb.ToString(), new { Id });
                if (post != null)
                {
                    return post;
                }
                else
                {
                    return new PostDto();
                }
            }
        }


        public async Task<int> InsertPostCommentAsync(Comment comment)
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
                Log.Error(ex.ToString());
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
                Log.Error(ex.ToString());
                return Enumerable.Empty<Comment>();
            }

        }
    }
}
