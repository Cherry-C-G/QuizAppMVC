using Microsoft.Data.SqlClient;
using QuizApp.Models;

namespace QuizApp.DAO
{
    public class CategoryDAO
    {
        private readonly IConfiguration _configuration;
        public CategoryDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// GetAll() - Retrieves all categories from the database.
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Categories", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    List<Category> categories = new List<Category>();
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.CategoryID = (int)reader["CategoryID"];
                        category.CategoryName = (string)reader["CategoryName"];

                        categories.Add(category);
                    }

                    return categories;
                }
            }
        }

        public void Insert(Category category)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Categories (CategoryName) VALUES (@CategoryName)", connection))
                {
                    command.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Update(Category category) - Updates an existing category in the database.
        /// </summary>
        /// <param name="category"></param>
        public void Update(Category category)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID", connection))
                {
                    command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    command.Parameters.AddWithValue("@CategoryID", category.CategoryID);

                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Delete(int id) - Deletes a category from the database by its ID.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Categories WHERE CategoryID = @CategoryID", connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", id);

                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// GetByID(int id) - Retrieves a single category by its ID from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Categories WHERE CategoryID = @CategoryID", connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Category category = new Category();
                        category.CategoryID = (int)reader["CategoryID"];
                        category.CategoryName = (string)reader["CategoryName"];
                        return category;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// GetByName(string name) - Retrieves a single category by its name from the database.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Category GetByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Categories WHERE CategoryName = @CategoryName", connection))
                {
                    command.Parameters.AddWithValue("@CategoryName", name);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Category category = new Category();
                        category.CategoryID = (int)reader["CategoryID"];
                        category.CategoryName = (string)reader["CategoryName"];
                        return category;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Create(Category category) - Inserts a new category into the database.
        /// </summary>
        /// <param name="category"></param>
        public void Create(Category category)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Categories (CategoryName) VALUES (@CategoryName)", connection))
                {
                    command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// IsCategoryExist(string name) - check whether a category already exists in the database or not.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsCategoryExist(string name)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Categories WHERE CategoryName = @CategoryName", connection))
                {
                    command.Parameters.AddWithValue("@CategoryName", name);

                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// Randomize(List<Category> categories) - Randomize a list of categories.
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        public List<Category> Randomize(List<Category> categories)
        {
            Random rnd = new Random();
            return categories.OrderBy(x => rnd.Next()).ToList();
        }

        /// <summary>
        /// GetAllQuestionsByCategory(int id) - Retrieves all questions by category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Question> GetAllQuestionsByCategory(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Questions WHERE CategoryID = @CategoryID", connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", id);

                    SqlDataReader reader = command.ExecuteReader();
                    List<Question> questions = new List<Question>();

                    while (reader.Read())
                    {
                        Question question = new Question();
                        question.QuestionID = (int)reader["QuestionID"];
                        question.CategoryID = (int)reader["CategoryID"];
                        question.QuestionText = (string)reader["QuestionText"];
                        question.QuestionType = (string)reader["QuestionType"];
                        questions.Add(question);
                    }

                    return questions;
                }
            }

        }
    }
}

