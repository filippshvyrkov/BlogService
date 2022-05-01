using BlogServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogServiceAPI.Repos
{
    public interface ICategoryRepo
    {
        public Category GetById(long id);
        public List<Category> GetAll();
    }
}
