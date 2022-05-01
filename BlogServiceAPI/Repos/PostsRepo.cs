using BlogServiceAPI.Configuration;
using BlogServiceAPI.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogServiceAPI.Repos
{
    public class PostsRepo : IPostsRepo
    {
        public Post Create(Post post)
        {            
            string connectionString = DatabaseConfiguration.DatabaseConnectionString;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string queryString = "insert into post(category_id, author_name, title, body, created_at) values(@categoryid, @authorName, @title, @body, now()) returning id";
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@categoryid", post.CategoryId);
                    command.Parameters.AddWithValue("@authorName", post.AuthorName);
                    command.Parameters.AddWithValue("@title", post.Title);
                    command.Parameters.AddWithValue("@body", post.Body);                    
                    connection.Open();
                    post.Id = (int) command.ExecuteScalar();
                }
            }
            return post;
        }
        
        public Post GetById(long id)
        {
            Post post = null;
            string connectionString = DatabaseConfiguration.DatabaseConnectionString;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string queryString = "select id, category_id, author_name, title, body, created_at from post where id = @id";
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
                                post = new Post();
                                post.Id = reader.GetInt64(0);
                                post.CategoryId = reader.GetInt64(1);
                                post.AuthorName = reader.GetString(2);
                                post.Title = reader.GetString(3);
                                post.Body = reader.GetString(4);
                                post.CreatedAt = reader.GetDateTime(5);                                
                            }
                        }
                    }
                }
            }
            return post;
        }

        public List<Post> GetAll()
        {
            List<Post> posts = new List<Post>();
            string connectionString = DatabaseConfiguration.DatabaseConnectionString;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string queryString = "select id, category_id, author_name, title, body, created_at from post order by created_at desc";
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {                                
                                Post post = new Post();
                                post.Id = reader.GetInt64(0);
                                post.CategoryId = reader.GetInt64(1);
                                post.AuthorName = reader.GetString(2);
                                post.Title = reader.GetString(3);
                                post.Body = reader.GetString(4);                               
                                post.CreatedAt = reader.GetDateTime(5);                               
                                posts.Add(post);
                            }
                        }
                    }
                }
            }
            return posts;
        }

        public List<Post> GetPage(int page, int pageSize)
        {
            int offset = (page - 1) * pageSize;
            List<Post> posts = new List<Post>();
            string connectionString = DatabaseConfiguration.DatabaseConnectionString;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string queryString = "select id, category_id, author_name, title, body, created_at from post order by created_at desc limit @pageSize offset @offset";
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Post post = new Post();
                                post.Id = reader.GetInt64(0);
                                post.CategoryId = reader.GetInt64(1);
                                post.AuthorName = reader.GetString(2);
                                post.Title = reader.GetString(3);
                                post.Body = reader.GetString(4);
                                post.CreatedAt = reader.GetDateTime(5);
                                posts.Add(post);
                            }
                        }
                    }
                }
            }
            return posts;
        }

        public int GetPostsCount()
        {
            int count = 0;
            string connectionString = DatabaseConfiguration.DatabaseConnectionString;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string queryString = "select count(*) from post";
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {                    
                    connection.Open();
                    count = (int)(long)command.ExecuteScalar();
                }
            }
            return count;
        }
    }
}
