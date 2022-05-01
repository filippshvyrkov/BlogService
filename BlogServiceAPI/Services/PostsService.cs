using BlogServiceAPI.Models;
using BlogServiceAPI.Repos;
using BlogServiceAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogServiceAPI.Services
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepo _postsRepo;
        private readonly ICategoryRepo _categoryRepo;
        public PostsService(IPostsRepo postsRepo, ICategoryRepo categoryRepo)
        {
            _postsRepo = postsRepo;
            _categoryRepo = categoryRepo;
        }

        public NewPost CreatePost(NewPost model)
        {
            Category foundCategory = _categoryRepo.GetById(model.CategoryId);
            if(foundCategory == null)
            {
                model.Created = false;
                model.ErrorMessage = "Invalid category ID.";
                return model;
            }

            Post newPost = new Post()
            {
                CategoryId = model.CategoryId,
                AuthorName = model.AuthorName,
                Title = model.Title,
                Body = model.Body
            };

            Post createdPost = _postsRepo.Create(newPost);

            if(createdPost.Id == 0)
            {
                model.Created = false;
                model.ErrorMessage = "An error has occurred while creating the post.";
                return model;
            }

            model.Id = createdPost.Id;
            model.Created = true;

            return model;
        }

        public List<PostViewModel> GetAllPostViewModels()
        {
            List<Post> allPosts = _postsRepo.GetAll();
            return MapPostViewModels(allPosts);
        }

        public PostViewModel GetPostById(long id)
        {
            Post post = _postsRepo.GetById(id);
            return MapPostViewModel(post);
        }

        public PostsPage GetPostsPage(int page, int pageSize)
        {
            PostsPage postsPage = new PostsPage();
            if (page <= 0)
            {
                page = 1;
            }
            int pageCount = 0;
            int totalPosts = _postsRepo.GetPostsCount();
            if (totalPosts % pageSize == 0)
            {
                pageCount = totalPosts / pageSize;
            }
            if (totalPosts % pageSize != 0)
            {
                pageCount = (totalPosts / pageSize) + 1;
            }
            if (pageCount < page)
            {
                page = pageCount;
            }
            List<Post> posts = _postsRepo.GetPage(page, pageSize);
            List<PostViewModel> postViewModels = MapPostViewModels(posts);
            postsPage.Posts = postViewModels;
            postsPage.Total = totalPosts;
            postsPage.PageNumber = page;
            return postsPage;
        }        

        private List<PostViewModel> MapPostViewModels(List<Post> posts)
        {
            List<PostViewModel> postViewModels = new List<PostViewModel>();
            foreach (var post in posts)
            {
                PostViewModel postViewModel = MapPostViewModel(post);
                postViewModels.Add(postViewModel);
            }
            return postViewModels;
        }

        private PostViewModel MapPostViewModel(Post post)
        {
            PostViewModel postViewModel = new PostViewModel()
            {
                Id = post.Id,
                CategoryId = post.CategoryId,
                AuthorName = post.AuthorName,
                Title = post.Title,
                Body = post.Body
            };
            return postViewModel;
        }
    }
}
