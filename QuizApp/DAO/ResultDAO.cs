using Microsoft.Data.SqlClient;
using QuizApp.Models;

namespace QuizApp.DAO
{
    public class ResultDAO
    {
        private readonly IConfiguration _configuration;
        public ResultDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// GetByID(int id) - Retrieves a specific result by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result GetByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Results WHERE ResultID = @ResultID", connection))
                {
                    command.Parameters.AddWithValue("@ResultID", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Result result = new Result();
                        result.ResultID = (int)reader["ResultID"];
                        result.UserID = (int)reader["UserID"];
                        result.QuizID = (int)reader["QuizID"];
                        result.StartTime = (DateTime)reader["StartTime"];
                        result.EndTime = (DateTime)reader["EndTime"];
                        result.Score = (int)reader["Score"];

                        return result;
                    }
                    return null;
                }
            }
        }
        /// <summary>
        /// GetByUserID(int userID) - Retrieves all results for a specific user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Result> GetByUserID(int userID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Results WHERE UserID = @UserID", connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    SqlDataReader reader = command.ExecuteReader();

                    List<Result> results = new List<Result>();
                    while (reader.Read())
                    {
                        Result result = new Result();
                        result.ResultID = (int)reader["ResultID"];
                        result.UserID = (int)reader["UserID"];
                        result.QuizID = (int)reader["QuizID"];
                        result.StartTime = (DateTime)reader["StartTime"];
                        result.EndTime = (DateTime)reader["EndTime"];
                        result.Score = (int)reader["Score"];

                        results.Add(result);
                    }
                    return results;
                }
            }
        }
        /// <summary>
        ///  GetByQuizID(int quizID) - Retrieves all results for a specific quiz
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>

        public List<Result> GetByQuizID(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Results WHERE QuizID = @QuizID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);

                    SqlDataReader reader = command.ExecuteReader();

                    List<Result> results = new List<Result>();
                    while (reader.Read())
                    {
                        Result result = new Result();
                        result.ResultID = (int)reader["ResultID"];
                        result.UserID = (int)reader["UserID"];
                        result.QuizID = (int)reader["QuizID"];
                        result.StartTime = (DateTime)reader["StartTime"];
                        result.EndTime = (DateTime)reader["EndTime"];
                        result.Score = (int)reader["Score"];

                        results.Add(result);
                    }
                    return results;
                }
            }
        }
        /// <summary>
        /// GetAll() - Retrieves all results
        /// </summary>
        /// <returns></returns>
        public List<Result> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Results", connection))
                {

                    SqlDataReader reader = command.ExecuteReader();

                    List<Result> results = new List<Result>();
                    while (reader.Read())
                    {
                        Result result = new Result();
                        result.ResultID = (int)reader["ResultID"];
                        result.UserID = (int)reader["UserID"];
                        result.QuizID = (int)reader["QuizID"];
                        result.StartTime = (DateTime)reader["StartTime"];
                        result.EndTime = (DateTime)reader["EndTime"];
                        result.Score = (int)reader["Score"];

                        results.Add(result);
                    }
                    return results;
                }
            }
        }
        /// <summary>
        /// Insert(Result result) - Inserts a new result into the database
        /// </summary>
        /// <param name="result"></param>
        public void Insert(Result result)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Results (UserID, QuizID, StartTime, EndTime, Score) VALUES (@UserID, @QuizID, @StartTime, @EndTime, @Score)", connection))
                {
                    command.Parameters.AddWithValue("@UserID", result.UserID);
                    command.Parameters.AddWithValue("@QuizID", result.QuizID);
                    command.Parameters.AddWithValue("@StartTime", result.StartTime);
                    command.Parameters.AddWithValue("@EndTime", result.EndTime);
                    command.Parameters.AddWithValue("@Score", result.Score);

                    command.ExecuteNonQuery();
                }
            }


        }
        /// <summary>
        /// Update(Result result) - Updates an existing result in the database
        /// </summary>
        /// <param name="result"></param>
        public void Update(Result result)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Results SET UserID = @UserID, QuizID = @QuizID, StartTime = @StartTime, EndTime = @EndTime, Score = @Score WHERE ResultID = @ResultID", connection))
                {
                    command.Parameters.AddWithValue("@UserID", result.UserID);
                    command.Parameters.AddWithValue("@QuizID", result.QuizID);
                    command.Parameters.AddWithValue("@StartTime", result.StartTime);
                    command.Parameters.AddWithValue("@EndTime", result.EndTime);
                    command.Parameters.AddWithValue("@Score", result.Score);
                    command.Parameters.AddWithValue("@ResultID", result.ResultID);

                    command.ExecuteNonQuery();
                }
            }

        }
        /// <summary>
        /// Delete(int id) - Deletes a specific result by its ID
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM Results WHERE ResultID = @ResultID", connection))
                {
                    command.Parameters.AddWithValue("@ResultID", id);

                    command.ExecuteNonQuery();
                }
            }

        }
        /// <summary>
        /// GetAvgScore(int quizID) - Retrieves the average score for a specific quiz
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>
        public double GetAvgScore(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT AVG(Score) FROM Results WHERE QuizID = @QuizID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);

                    return (double)command.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// GetHighScore(int quizID) - Retrieves the highest score for a specific quiz
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>
        public int GetHighScore(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT MAX(Score) FROM Results WHERE QuizID = @QuizID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);

                    return (int)command.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// GetLowScore(int quizID) - Retrieves the lowest score for a specific quiz
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>
        public int GetLowestScore(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT MIN(Score) FROM Results WHERE QuizID = @QuizID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);

                    return (int)command.ExecuteScalar();
                }
            }

        }
        /// <summary>
        /// GetResultsByDate(DateTime startDate, DateTime endDate) - Retrieves all results within a specific date range
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Result> GetRecentResults(int count)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT TOP (@Count) * FROM Results ORDER BY EndTime DESC", connection))
                {
                    command.Parameters.AddWithValue("@Count", count);

                    SqlDataReader reader = command.ExecuteReader();
                    List<Result> results = new List<Result>();

                    while (reader.Read())
                    {
                        Result result = new Result();
                        result.ResultID = (int)reader["ResultID"];
                        result.UserID = (int)reader["UserID"];
                        result.QuizID = (int)reader["QuizID"];
                        result.StartTime = (DateTime)reader["StartTime"];
                        result.EndTime = (DateTime)reader["EndTime"];
                        result.Score = (int)reader["Score"];

                        results.Add(result);
                    }

                    return results;
                }
            }

        }
        /// <summary>
        /// GetCount() - Retrieves the total number of results in the database
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="quizID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public int GetCount(int userID, int quizID, DateTime startDate, DateTime endDate)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Results WHERE UserID = @UserID AND QuizID = @QuizID AND StartTime >= @StartTime AND EndTime <= @EndTime", connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@QuizID", quizID);
                    command.Parameters.AddWithValue("@StartTime", startDate);
                    command.Parameters.AddWithValue("@EndTime", endDate);

                    return (int)command.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// Search(string keyword) - searches for results by keyword.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="quizID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<Result> Search(int userID, int quizID, DateTime startDate, DateTime endDate)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Results WHERE UserID = @UserID AND QuizID = @QuizID AND StartTime >= @StartTime AND EndTime <= @EndTime", connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@QuizID", quizID);
                    command.Parameters.AddWithValue("@StartTime", startDate);
                    command.Parameters.AddWithValue("@EndTime", endDate);

                    SqlDataReader reader = command.ExecuteReader();

                    List<Result> results = new List<Result>();
                    while (reader.Read())
                    {
                        Result result = new Result();
                        result.ResultID = (int)reader["ResultID"];
                        result.UserID = (int)reader["UserID"];
                        result.QuizID = (int)reader["QuizID"];
                        result.StartTime = (DateTime)reader["StartTime"];
                        result.EndTime = (DateTime)reader["EndTime"];
                        result.Score = (int)reader["Score"];

                        results.Add(result);
                    }

                    return results;
                }
            }
        }

    }
}
