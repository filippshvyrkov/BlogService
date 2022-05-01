using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogServiceAPI.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
