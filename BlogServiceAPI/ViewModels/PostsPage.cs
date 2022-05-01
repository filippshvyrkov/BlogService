using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogServiceAPI.ViewModels
{
    public class PostsPage
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public List<PostViewModel> Posts {get; set;}
    }
}
