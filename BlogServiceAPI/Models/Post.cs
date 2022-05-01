using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogServiceAPI.Models
{
    public class Post
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
