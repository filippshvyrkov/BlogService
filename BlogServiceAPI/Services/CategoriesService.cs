using BlogServiceAPI.Models;
using BlogServiceAPI.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogServiceAPI.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoriesService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public List<Category> All()
        {
            List<Category> categories = _categoryRepo.GetAll();
            return categories;
        }
    }
}
