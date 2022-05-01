using BlogServiceAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogServiceAPI.Services
{
    public interface IPostsService
    {
        public NewPost CreatePost(NewPost model);
        public List<PostViewModel> GetAllPostViewModels();
        public PostViewModel GetPostById(long id);
        public PostsPage GetPostsPage(int page, int pageSize);        
    }
}
