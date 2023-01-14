using Microsoft.Data.SqlClient;
using QuizApp.Models;

namespace QuizApp.DAO
{
    public class FeedbackDAO
    {
        private readonly IConfiguration _configuration;
        public FeedbackDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Create (Insert)
        /// </summary>
        /// <param name="feedback"></param>
        public void Create(Feedback feedback)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Feedback (UserID, Rating, FeedbackText) VALUES (@UserID, @Rating, @FeedbackText)", connection))
                {
                    command.Parameters.AddWithValue("@UserID", feedback.UserID);
                    command.Parameters.AddWithValue("@Rating", feedback.Rating);
                    command.Parameters.AddWithValue("@FeedbackText", feedback.FeedbackText);

                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Read (Get/Retrieve) - GetAll, GetByID, GetByUserID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Feedback GetByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Feedback WHERE FeedbackID = @FeedbackID", connection))
                {
                    command.Parameters.AddWithValue("@FeedbackID", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Feedback feedback = new Feedback();
                        feedback.FeedbackID = (int)reader["FeedbackID"];
                        feedback.UserID = (int)reader["UserID"];
                        feedback.Rating = (int)reader["Rating"];
                        feedback.FeedbackText = (string)reader["FeedbackText"];

                        return feedback;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="feedback"></param>
        public void Update(Feedback feedback)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE Feedback SET UserID = @UserID, Rating = @Rating, FeedbackText = @FeedbackText WHERE FeedbackID = @FeedbackID", connection))
                {
                    command.Parameters.AddWithValue("@FeedbackID", feedback.FeedbackID);
                    command.Parameters.AddWithValue("@UserID", feedback.UserID);
                    command.Parameters.AddWithValue("@Rating", feedback.Rating);
                    command.Parameters.AddWithValue("@FeedbackText", feedback.FeedbackText);

                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Feedback WHERE FeedbackID = @FeedbackID", connection))
                {
                    command.Parameters.AddWithValue("@FeedbackID", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Read (Get/Retrieve) - GetAll, GetByID, GetByUserID
        /// </summary>
        /// <returns></returns>
        public List<Feedback> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Feedback", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    List<Feedback> feedbacks = new List<Feedback>();

                    while (reader.Read())
                    {
                        Feedback feedback = new Feedback();
                        feedback.FeedbackID = (int)reader["FeedbackID"];
                        feedback.UserID = (int)reader["UserID"];
                        feedback.Rating = (int)reader["Rating"];
                        feedback.FeedbackText = (string)reader["FeedbackText"];
                        feedbacks.Add(feedback);
                    }

                    return feedbacks;
                }
            }
        }
        /// <summary>
        /// Read (Get/Retrieve) - GetAll, GetByID, GetByUserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Feedback> GetByUserID(int userID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Feedback WHERE UserID = @UserID", connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    SqlDataReader reader = command.ExecuteReader();
                    List<Feedback> feedbacks = new List<Feedback>();
                    while (reader.Read())
                    {
                        Feedback feedback = new Feedback();
                        feedback.FeedbackID = (int)reader["FeedbackID"];
                        feedback.UserID = (int)reader["UserID"];
                        feedback.Rating = (int)reader["Rating"];
                        feedback.FeedbackText = (string)reader["FeedbackText"];
                        feedbacks.Add(feedback);
                    }
                    return feedbacks;
                }
            }
        }
        /// <summary>
        /// GetAvgRating
        /// </summary>
        /// <returns></returns>
        public double GetAvgRating()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT AVG(Rating) FROM Feedback", connection))
                {
                    return (double)command.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// GetFeedbackByUser
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Feedback GetFeedbackByUser(int userID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Feedback WHERE UserID = @UserID", connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Feedback feedback = new Feedback();
                        feedback.FeedbackID = (int)reader["FeedbackID"];
                        feedback.UserID = (int)reader["UserID"];
                        feedback.Rating = (int)reader["Rating"];
                        feedback.FeedbackText = (string)reader["FeedbackText"];
                        return feedback;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// GetFeedbackCount
        /// </summary>
        /// <returns></returns>
        public int GetFeedbackCount()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Feedback", connection))
                {
                    return (int)command.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// GetLatestFeedback
        /// </summary>
        /// <returns></returns>
        public Feedback GetLatestFeedback()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM Feedback ORDER BY FeedbackID DESC", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Feedback feedback = new Feedback();
                        feedback.FeedbackID = (int)reader["FeedbackID"];
                        feedback.UserID = (int)reader["UserID"];
                        feedback.Rating = (int)reader["Rating"];
                        feedback.FeedbackText = reader["FeedbackText"].ToString();
                        return feedback;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// GetFeedbackByRating
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        public List<Feedback> GetFeedbackByRating(int rating)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Feedback WHERE Rating = @Rating", connection))
                {
                    command.Parameters.AddWithValue("@Rating", rating);

                    SqlDataReader reader = command.ExecuteReader();
                    List<Feedback> feedbacks = new List<Feedback>();

                    while (reader.Read())
                    {
                        Feedback feedback = new Feedback();
                        feedback.FeedbackID = (int)reader["FeedbackID"];
                        feedback.UserID = (int)reader["UserID"];
                        feedback.Rating = (int)reader["Rating"];
                        feedback.FeedbackText = (string)reader["FeedbackText"];
                        feedbacks.Add(feedback);
                    }

                    return feedbacks;
                }
            }
        }
        /// <summary>
        /// Search (filter) feedback by keyword or date.
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<Feedback> Search(string keyword, DateTime? startDate, DateTime? endDate)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Feedback WHERE FeedbackText LIKE @Keyword AND (FeedbackDate BETWEEN @StartDate AND @EndDate)", connection))
                {
                    command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataReader reader = command.ExecuteReader();
                    List<Feedback> feedbacks = new List<Feedback>();

                    while (reader.Read())
                    {
                        Feedback feedback = new Feedback();
                        feedback.FeedbackID = (int)reader["FeedbackID"];
                        feedback.UserID = (int)reader["UserID"];
                        feedback.Rating = (int)reader["Rating"];
                        feedback.FeedbackText = (string)reader["FeedbackText"];
                        feedbacks.Add(feedback);
                    }

                    return feedbacks;
                }
            }
        }


    }
}
