using BlogServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogServiceAPI.Services
{
    public interface ICategoriesService
    {
        public List<Category> All();
    }
}
