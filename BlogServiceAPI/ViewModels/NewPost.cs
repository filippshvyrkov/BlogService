using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogServiceAPI.ViewModels
{
    public class NewPost
    {
        public long Id { get; set; }
        [Required]
        public long CategoryId { get; set; }
        [Required]        
        public string AuthorName { get; set; }
        [Required]
        [MinLength(0)]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MinLength(0)]
        [MaxLength(750)]
        public string Body { get; set; }
        public bool Created { get; set; }
        public string ErrorMessage { get; set; }
    }
}
