using DaisyPets.Core.Application.Interfaces.Services.Blog;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;

namespace DaisyPets.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IBlogService _service;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IBlogService service, ILogger<PostsController> logger)
        {
            _service = service;
            _logger = logger;
        }


        /// <summary>
        /// Criar post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(PostDto post)
        {
            var location = GetControllerActionNames();
            try
            {

                if (post is null)
                {
                    return BadRequest();
                }

                var validator = new PostValidator();
                var result = validator.Validate(post);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                var insertedId = await _service.InsertPostAsync(post);
                var viewPost = await _service.FindPostByIdAsync(insertedId);
                var actionReturned = CreatedAtAction(nameof(Get), new { id = viewPost.Id }, viewPost);


                return Ok(new { Id = insertedId });

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Criar post
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost("AddComment")]
        public async Task<IActionResult> InsertComent(CommentDto comment)
        {
            var location = GetControllerActionNames();
            try
            {

                if (comment is null)
                {
                    return BadRequest();
                }

                //var validator = new PostValidator();
                //var result = validator.Validate(post);
                //if (result.IsValid == false)
                //{
                //    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                //    return BadRequest(errorMessages);
                //}

                var insertedId = await _service.InsertPostCommentAsync(comment);
                var viewPost = await _service.FindPostByIdAsync(insertedId);
                var actionReturned = CreatedAtAction(nameof(Get), new { id = viewPost.Id }, viewPost);


                return Ok(new { Id = insertedId });

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }


        /// <summary>
        /// Atualiza post
        /// </summary>
        /// <param name="Id">Id do post</param>
        /// <param name="post">Dados do post</param>
        /// <returns></returns>
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Update(int Id, [FromBody] PostDto post)
        {
            var location = GetControllerActionNames();

            try
            {

                if (post == null)
                {
                    string msg = "O Post passado como paràmetro é incorreto.";
                    _logger.LogWarning(msg);
                    return BadRequest(msg);
                }

                if (Id != post.Id)
                {
                    return BadRequest($"O id ({Id}) passado como paràmetro é incorreto");
                }

                var viewPost = await _service.GetPostAsync(Id);
                if (viewPost == null)
                {
                    return NotFound("Post não foi encontrado");
                }

                var validator = new PostValidator();
                var result = validator.Validate(post);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                await _service.UpdatePostAsync(Id, post);
                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, "Erro ao atualizar post (API)");
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Delete Post
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var viewPost = await  _service.GetPostAsync(Id);
                if (viewPost == null)
                {
                    return NotFound("Post não foi encontrado");
                }

                await _service.DeletePostAsync(Id);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }


        /// <summary>
        /// Get post by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {

            // TODO - duplicado com GetById...

            var location = GetControllerActionNames();

            try
            {
                var post = await _service.FindPostByIdAsync(Id);
                if (post is null)
                {
                    return NotFound();
                }

                return Ok(post);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Devolve todo os posts
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllPosts")]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                var listOfPosts = await _service.GetAllPostsVMAsync();
                if (listOfPosts is null)
                { return NotFound(); }

                return Ok(listOfPosts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados dos posts não foram encontrados");
            }
        }

        /// <summary>
        /// Devolve comentários de um post
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("PostComments/{Id:int}")]
        public async Task<IActionResult> GetCommentsById(int Id)
        {
            try
            {
                var listOfComments = await _service.GetPostComments(Id);
                if (listOfComments is null)
                { return NotFound(); }

                return Ok(listOfComments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados dos comentários não foram encontrados");
            }
        }


        /// <summary>
        /// Validate Appointment
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost("ValidatePost")]
        public IActionResult ValidatePost([FromBody] PostDto post)
        {
            // Create validator instance (or inject it)
            var PostValidator = new PostValidator();

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = PostValidator.Validate(post);

            if (result.IsValid)
            {
                return Ok(post);
            }

            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessages);
        }


        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;

            return $"{controller} - {action}";
        }

        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, $"Algo de errado ocorreu ({message}). Contacte o Administrador");
        }


    }
}
