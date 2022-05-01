using BlogServiceAPI.Configuration;
using BlogServiceAPI.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogServiceAPI.Repos
{
    public class CategoryRepo : ICategoryRepo
    {
        public Category GetById(long id)
        {
            Category category = null;
            string connectionString = DatabaseConfiguration.DatabaseConnectionString;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string queryString = "select id, category_name, created_at from category where id = @id";
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                category = new Category();
                                category.Id = reader.GetInt64(0);
                                category.CategoryName = reader.GetString(1);                                
                                category.CreatedAt = reader.GetDateTime(2);
                            }
                        }
                    }
                }
            }
            return category;
        }

        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();
            string connectionString = DatabaseConfiguration.DatabaseConnectionString;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string queryString = "select id, category_name, created_at from category order by created_at desc";
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Category category = new Category();
                                category.Id = reader.GetInt64(0);
                                category.CategoryName = reader.GetString(1);
                                category.CreatedAt = reader.GetDateTime(2);
                                categories.Add(category);
                            }
                        }
                    }
                }
            }
            return categories;
        }
    }
}
