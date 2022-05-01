using BlogServiceAPI.Models;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IPostsService _postsService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoriesService categoriesService, IPostsService postsService, ILogger<CategoriesController> logger)
        {
            _categoriesService = categoriesService;
            _postsService = postsService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                List<Category> categories = _categoriesService.All();
                return Ok(categories);
            }
            catch (Exception exception)
            {
                _logger.LogInformation(exception.Message);
                return NoContent();
            }
        }

        [HttpGet]
        [Route("{id}/posts")]
        public ActionResult GetPosts(long id)
        {
            try
            {
                List<PostViewModel> posts = _postsService.GetPostViewModelsByCategoryId(id);
                return Ok(posts);
            }
            catch (Exception exception)
            {
                _logger.LogInformation(exception.Message);
                return NoContent();
            }
        }
    }
}
