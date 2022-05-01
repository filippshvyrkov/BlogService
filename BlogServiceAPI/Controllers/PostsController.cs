using BlogServiceAPI.Models;
using BlogServiceAPI.Repos;
using BlogServiceAPI.Services;
using BlogServiceAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IPostsService postsService, ILogger<PostsController> logger)
        {
            _postsService = postsService;
            _logger = logger;
        }
        [HttpPost]
        public IActionResult CreatePost([FromBody] NewPost model)
        {
            try
            {
                NewPost newPost = _postsService.CreatePost(model);
                if (!newPost.Created)
                {
                    return BadRequest(newPost);
                }
                return CreatedAtAction(nameof(GetPost), new { id = newPost.Id }, newPost);
                
            }
            catch (Exception exception)
            {
                _logger.LogInformation(exception.Message);
                return NoContent();
            }
        }

        [HttpGet]        
        public ActionResult GetPosts()
        {
            try
            {
                List<PostViewModel> posts = _postsService.GetAllPostViewModels();
                return Ok(posts);
            }
            catch (Exception exception)
            {
                _logger.LogInformation(exception.Message);
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Post> GetPost(long id)
        {
            try
            {
                PostViewModel post = _postsService.GetPostById(id);
                if(post == null)
                {
                    return NotFound();
                }
                return Ok(post);
            }
            catch (Exception exception)
            {
                _logger.LogInformation(exception.Message);
                return NoContent();
            }
        }

        [HttpGet]
        [Route("page")]
        public ActionResult GetPostsPage(int page, int pageSize)
        {
            try
            {
                PostsPage postsPage = _postsService.GetPostsPage(page, pageSize);
                return Ok(postsPage);
            }
            catch (Exception exception)
            {
                _logger.LogInformation(exception.Message);
                return NoContent();
            }
        }
    }
}
