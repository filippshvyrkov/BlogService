using BlogServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogServiceAPI.Repos
{
    public interface IPostsRepo
    {
        public Post Create(Post post);
        public Post GetById(long Id);
        public List<Post> GetAll();
        public List<Post> GetPage(int page, int pageSize);
        public int GetPostsCount();
        public List<Post> GetByCategoryId(long categoryId);
    }
}
