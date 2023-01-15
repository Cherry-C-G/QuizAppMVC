using Microsoft.Data.SqlClient;
using QuizApp.Models;

namespace QuizApp.DAO
{
    public class UsersDAO
    {
        private readonly IConfiguration _configuration;
        public UsersDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Create(Users user)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Users (Email, Password, FirstName, LastName, DateOfBirth, Role) VALUES (@Email, @Password, @FirstName, @LastName, @DateOfBirth, @Role)", connection))
                {
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                    command.Parameters.AddWithValue("@Role", user.Role);

                    command.ExecuteNonQuery();
                }
            }
        }


        public void Add(Users user)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Users (Email, Password, FirstName, LastName, DateOfBirth, Role) VALUES (@Email, @Password, @FirstName, @LastName, @DateOfBirth, @Role)", connection))
                {
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                    command.Parameters.AddWithValue("@Role", user.Role);

                    command.ExecuteNonQuery();
                }
            }
        }

        public Users Get(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE UserId = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Users
                            {
                                UserID = (int)reader["UserId"],
                                Email = (string)reader["Email"],
                                Password = (string)reader["Password"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                DateOfBirth = (DateTime)reader["DateOfBirth"],
                                Role = (string)reader["Role"]
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Update(Users user) - Updates an existing user in the database
        /// </summary>
        /// <param name="user"></param>
        public void Update(Users user)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE Users SET Email = @Email, Password = @Password, FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Role = @Role WHERE UserId = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                    command.Parameters.AddWithValue("@Role", user.Role);
                    command.Parameters.AddWithValue("@UserId", user.UserID);

                    command.ExecuteNonQuery();
                }
            }
        }

      

        public string GetFullName(int userID)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var command = new SqlCommand("SELECT FullName FROM Users WHERE UserID = @userID", connection);
                command.Parameters.AddWithValue("@userID", userID);

                var fullName = (string)command.ExecuteScalar();

                return fullName;
            }
        }
    


    /// <summary>
    /// Delete(int id) - Deletes a specific user by their ID
    /// </summary>
    /// <param name="userId"></param>
    public void Delete(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Users WHERE UserId = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// GetAll() - Retrieves all users
        /// </summary>
        /// <returns></returns>
        public List<Users> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Users", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Users> users = new List<Users>();
                        while (reader.Read())
                        {
                            users.Add(new Users
                            {
                                UserID = (int)reader["UserId"],
                                Email = (string)reader["Email"],
                                Password = (string)reader["Password"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                DateOfBirth = (DateTime)reader["DateOfBirth"],
                                Role = (string)reader["Role"]
                            });
                        }
                        return users;
                    }
                }
            }
        }
        /// <summary>
        /// GetByEmail(string email) - Retrieves a specific user by their email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Users GetByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Users user = new Users();
                            user.UserID = (int)reader["UserId"];
                            user.FirstName = (string)reader["FirstName"];
                            user.LastName = (string)reader["LastName"];
                            user.Email = (string)reader["Email"];
                            user.Password = (string)reader["Password"];
                            user.DateOfBirth = (DateTime)reader["DateOfBirth"];
                            user.Role = (string)reader["Role"];
                            return user;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Insert(Users user) - Inserts a new user into the database
        /// </summary>
        /// <param name="user"></param>
        public void Insert(Users user)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Users (Email, Password, FirstName, LastName, DateOfBirth, Role) VALUES (@Email, @Password, @FirstName, @LastName, @DateOfBirth, @Role)", connection))
                {
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                    command.Parameters.AddWithValue("@Role", user.Role);

                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// IsEmailExist(string email) - Checks if a specific email address already exists in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmailExist(string email)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

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
        /// IsUserValid(string email, string password) - Validates a user's email and password for login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsUserValid(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email AND Password = @Password", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        /// <summary>
        /// UpdatePassword(string email, string newPassword) - Updates a user's password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPassword"></param>
        public void UpdatePassword(string email, string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE Users SET Password = @Password WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", newPassword);

                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// UpdateRole(int userID, string role) - Updates a user's role (e.g. from "User" to "Admin")
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="role"></param>
        public void UpdateRole(int userID, string role)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Users SET Role = @Role WHERE UserID = @UserID", connection))
                {
                    command.Parameters.AddWithValue("@Role", role);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Search(string keyword) - Search for users by keyword.
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<Users> Search(string keyword)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Email LIKE @Keyword OR FirstName LIKE @Keyword OR LastName LIKE @Keyword", connection))
                {
                    command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    SqlDataReader reader = command.ExecuteReader();
                    List<Users> users = new List<Users>();
                    while (reader.Read())
                    {
                        Users user = new Users();
                        user.UserID = (int)reader["UserID"];
                        user.Email = (string)reader["Email"];
                        user.Password = (string)reader["Password"];
                        user.FirstName = (string)reader["FirstName"];
                        user.LastName = (string)reader["LastName"];
                        user.DateOfBirth = (DateTime)reader["DateOfBirth"];
                        user.Role = (string)reader["Role"];
                        users.Add(user);
                    }
                    return users;
                }
            }
        }


    }
}
