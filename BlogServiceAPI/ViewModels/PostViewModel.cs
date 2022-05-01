using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogServiceAPI.ViewModels
{
    public class PostViewModel
    {
        public long Id { get; set; }        
        public long CategoryId { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string AuthorDisplayName => $"By {AuthorName}";
    }
}
