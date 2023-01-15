using Microsoft.Data.SqlClient;
using QuizApp.Models;

namespace QuizApp.DAO
{
    public class QuizDAO
    {
        private readonly IConfiguration _configuration;
        private readonly QuizQuestionDAO _quizQuestionDAO;
        private readonly ResultDAO _resultDAO;
        public QuizDAO(IConfiguration configuration, QuizQuestionDAO quizQuestionDAO, ResultDAO resultDAO)
        {
            _configuration = configuration;
            _quizQuestionDAO = quizQuestionDAO;
            _resultDAO = resultDAO;
        }

        public void Create(Quiz quiz)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Quizzes (QuizName, QuizType, TimeLimit) VALUES (@QuizName, @QuizType, @TimeLimit)", connection))
                {
                    command.Parameters.AddWithValue("@QuizName", quiz.QuizName);
                    command.Parameters.AddWithValue("@QuizType", quiz.QuizType);
                    command.Parameters.AddWithValue("@TimeLimit", quiz.TimeLimit);

                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// GetByID(int id) - Retrieves a specific quiz by its ID
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        public Quiz GetById(int quizId)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Quizzes WHERE QuizID = @QuizID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Quiz quiz = new Quiz();
                            quiz.QuizID = (int)reader["QuizID"];
                            quiz.QuizName = (string)reader["QuizName"];
                            quiz.QuizType = (string)reader["QuizType"];
                            quiz.TimeLimit = (int)reader["TimeLimit"];
                            return quiz;
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
        /// GetAll() - Retrieves all quizzes
        /// </summary>
        /// <returns></returns>
        public List<Quiz> GetAll()
        {
            List<Quiz> quizzes = new List<Quiz>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Quizzes", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Quiz quiz = new Quiz();
                            quiz.QuizID = (int)reader["QuizID"];
                            quiz.QuizName = (string)reader["QuizName"];
                            quiz.QuizType = (string)reader["QuizType"];
                            quiz.TimeLimit = (int)reader["TimeLimit"];
                            quizzes.Add(quiz);
                        }
                    }
                }
            }
            return quizzes;
        }

        /// <summary>
        /// Update(Quiz quiz) - Updates an existing quiz in the database
        /// </summary>
        /// <param name="quiz"></param>
        public void Update(Quiz quiz)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE Quizzes SET QuizName = @QuizName, QuizType = @QuizType, TimeLimit = @TimeLimit WHERE QuizID = @QuizID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quiz.QuizID);
                    command.Parameters.AddWithValue("@QuizName", quiz.QuizName);
                    command.Parameters.AddWithValue("@QuizType", quiz.QuizType);
                    command.Parameters.AddWithValue("@TimeLimit", quiz.TimeLimit);

                    command.ExecuteNonQuery();
                }
            }
        }

        public string GetQuizName(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT QuizName FROM Quizzes WHERE QuizID = @quizID", connection))
                {
                    command.Parameters.AddWithValue("@quizID", quizID);

                    return (string)command.ExecuteScalar();
                }
            }
        }


            /// <summary>
            /// Delete(int id) - Deletes a specific quiz by its ID
            /// </summary>
            /// <param name="quizId"></param>
            public void Delete(int quizId)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Quizzes WHERE QuizID = @QuizID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizId);

                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Insert(Quiz quiz) - Inserts a new quiz into the database
        /// </summary>
        /// <param name="quiz"></param>
        public void Insert(Quiz quiz)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Quiz (QuizName, QuizType, TimeLimit) VALUES (@QuizName, @QuizType, @TimeLimit)", connection))
                {
                    command.Parameters.AddWithValue("@QuizName", quiz.QuizName);
                    command.Parameters.AddWithValue("@QuizType", quiz.QuizType);
                    command.Parameters.AddWithValue("@TimeLimit", quiz.TimeLimit);

                    command.ExecuteNonQuery();
                }
            }

        }

        public void SubmitQuiz(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE Quiz SET IsCompleted = 1 WHERE QuizID = @QuizID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);
                    command.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// GetQuestions(int quizID) - Retrieves all questions for a specific quiz
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>
        public List<Question> GetQuestions(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT q.* FROM Questions q JOIN QuizQuestion qq ON q.QuestionID = qq.QuestionID WHERE qq.QuizID = @QuizID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);

                    SqlDataReader reader = command.ExecuteReader();
                    List<Question> questions = new List<Question>();

                    while (reader.Read())
                    {
                        Question question = new Question();
                        question.QuestionID = (int)reader["QuestionID"];
                        question.QuestionText = (string)reader["QuestionText"];
                        question.QuestionType = (string)reader["QuestionType"];

                        questions.Add(question);
                    }

                    return questions;
                }
            }

        }
        /// <summary>
        /// GetTimeRemaining(int quizID, int userID) - Retrieves the remaining time for a user to complete a quiz
        /// </summary>
        /// <param name="quizID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetTimeRemaining(int quizID, int userID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT DATEDIFF(SECOND, StartTime, GETDATE()) as TimeElapsed FROM Results WHERE QuizID = @QuizID AND UserID = @UserID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);
                    command.Parameters.AddWithValue("@UserID", userID);

                    int timeElapsed = (int)command.ExecuteScalar();

                    Quiz quiz = GetByID(quizID);
                    int timeRemaining = quiz.TimeLimit - timeElapsed;

                    return timeRemaining;
                }
            }


        }
        /// <summary>
        /// SubmitQuiz(int quizID, int userID) - Submits a quiz for a specific user
        /// </summary>
        /// <param name="quizID"></param>
        /// <param name="userID"></param>
        public void SubmitQuiz(int quizID, int userID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Results SET EndTime = GETDATE() WHERE QuizID = @QuizID AND UserID = @UserID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);
                    command.Parameters.AddWithValue("@UserID", userID);

                    command.ExecuteNonQuery();
                }
            }


        }
        /// <summary>
        /// CheckIfAllQuestionsAnswered(int quizID, int userID) - Checks if a user has answered all questions in a quiz
        /// </summary>
        /// <param name="quizID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool CheckIfAllQuestionsAnswered(int quizID, int userID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM QuizQuestion qq JOIN Results r ON qq.QuizID = r.QuizID JOIN Answers a ON qq.AnswerID = a.AnswerID WHERE r.QuizID = @QuizID AND r.UserID = @UserID AND a.IsCorrect = 1", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);
                    command.Parameters.AddWithValue("@UserID", userID);

                    int answeredQuestions = (int)command.ExecuteScalar();

                    using (SqlCommand command2 = new SqlCommand("SELECT COUNT(*) FROM QuizQuestion WHERE QuizID = @QuizID", connection))
                    {
                        command2.Parameters.AddWithValue("@QuizID", quizID);

                        int totalQuestions = (int)command2.ExecuteScalar();

                        return answeredQuestions == totalQuestions;
                    }
                }
            }
        }
        /// <summary>
        /// GetQuestionStyles(int quizID, int userID) - Retrieves the styles for answered and unanswered questions for a user
        /// </summary>
        /// <param name="quizID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Dictionary<int, string> GetQuestionStyles(int quizID, int userID, int currentQuestion)
        {
            Dictionary<int, string> styles = new Dictionary<int, string>();
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT qq.QuestionID, a.AnswerID FROM QuizQuestion qq LEFT JOIN Result r ON qq.QuizID = r.QuizID AND r.UserID = @UserID LEFT JOIN Answer a ON qq.AnswerID = a.AnswerID WHERE qq.QuizID = @QuizID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);
                    command.Parameters.AddWithValue("@UserID", userID);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int questionID = (int)reader["QuestionID"];
                        if (reader["AnswerID"] == DBNull.Value)
                        {
                            styles.Add(questionID, "unanswered");
                        }
                        else
                        {
                            styles.Add(questionID, "answered");
                        }
                    }
                }
            }
            return styles;
        }

        public List<string> GetQuestionStyles(int quizID, int currentQuestion)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT qq.QuizQuestionID, a.IsCorrect FROM QuizQuestion qq JOIN Results r ON qq.QuizID = r.QuizID JOIN Answers a ON qq.AnswerID = a.AnswerID WHERE r.QuizID = @QuizID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);

                    SqlDataReader reader = command.ExecuteReader();

                    List<string> questionStyles = new List<string>();

                    while (reader.Read())
                    {
                        if ((int)reader["QuizQuestionID"] == currentQuestion)
                        {
                            questionStyles.Add("current");
                        }
                        else if ((bool)reader["IsCorrect"])
                        {
                            questionStyles.Add("correct");
                        }
                        else
                        {
                            questionStyles.Add("incorrect");
                        }
                    }

                    return questionStyles;
                }
            }
        }
        /// <summary>
        /// GetShortAnswerQuestions(int quizID) - Retrieves all short answer questions for a quiz
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>
        public List<Question> GetShortAnswerQuestions(int quizID)
        {
            List<Question> shortAnswerQuestions = new List<Question>();
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT q.* FROM Questions q JOIN QuizQuestion qq ON q.QuestionID = qq.QuestionID WHERE qq.QuizID = @QuizID AND q.QuestionType = 'ShortAnswer'", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Question question = new Question();
                        question.QuestionID = (int)reader["QuestionID"];
                        question.QuestionText = (string)reader["QuestionText"];
                        question.QuestionType = (string)reader["QuestionType"];
                        shortAnswerQuestions.Add(question);
                    }
                }
            }
            return shortAnswerQuestions;
        }
        /// <summary>
        /// MarkQuestion(int quizID, int questionID, int userID) - Marks a question for a user
        /// </summary>
        /// <param name="quizID"></param>
        /// <param name="questionID"></param>
        /// <param name="userID"></param>
        public void MarkQuestion(int quizID, int questionID, int userID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE QuizQuestion SET IsMarked = 1 WHERE QuizID = @QuizID AND QuestionID = @QuestionID AND UserID = @UserID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);
                    command.Parameters.AddWithValue("@QuestionID", questionID);
                    command.Parameters.AddWithValue("@UserID", userID);

                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// GetTimer(int quizID) - Retrieves the timer for a quiz.
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>
        public int GetTimer(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT TimeLimit FROM Quiz WHERE QuizID = @QuizID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);

                    return (int)command.ExecuteScalar();
                }
            }
        }

        public Quiz GetByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Quizzes WHERE QuizID = @QuizID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Quiz quiz = new Quiz();
                        quiz.QuizID = (int)reader["QuizID"];
                        quiz.QuizName = (string)reader["QuizName"];
                        quiz.QuizType = (string)reader["QuizType"];
                        quiz.TimeLimit = (int)reader["TimeLimit"];

                        return quiz;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        //public int GetNavigatorQuestion(int quizID, int userID, int currentQuestion)
        //{
        //    using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        connection.Open();

        //        using (SqlCommand command = new SqlCommand("SELECT QuizQuestionID FROM QuizQuestion qq JOIN Results r ON qq.QuizID = r.QuizID JOIN Answers a ON qq.AnswerID = a.AnswerID WHERE r.QuizID = @QuizID AND r.UserID = @UserID AND a.IsCorrect = 1", connection))
        //        {
        //            command.Parameters.AddWithValue("@QuizID", quizID);
        //            command.Parameters.AddWithValue("@UserID", userID);

        //            SqlDataReader reader = command.ExecuteReader();

        //            List<int> answeredQuestions = new List<int>();

        //            while (reader.Read())
        //            {
        //                answeredQuestions.Add((int)reader["QuizQuestionID"]);
        //            }

        //            using (SqlCommand command2 = new SqlCommand("SELECT QuizQuestionID FROM QuizQuestion WHERE QuizID = @QuizID", connection))
        //            {
        //                command2.Parameters.AddWithValue("@QuizID", quizID);

        //                reader = command2.ExecuteReader();

        //                List<int> allQuestions = new List<int>();

        //                while (reader.Read())
        //                    allQuestions.Add((int)reader["QuizQuestionID"]);
        //            }
        //            int nextQuestion = currentQuestion + 1;
        //            int previousQuestion = currentQuestion - 1;

        //            if (!allQuestions.Contains(nextQuestion) || answeredQuestions.Contains(nextQuestion))
        //            {
        //                nextQuestion = -1;
        //            }

        //            if (!allQuestions.Contains(previousQuestion) || answeredQuestions.Contains(previousQuestion))
        //            {
        //                previousQuestion = -1;
        //            }

        //            if (previousQuestion != -1 && answeredQuestions.Contains(previousQuestion))
        //            {
        //                return previousQuestion;
        //            }
        //            else if (nextQuestion != -1)
        //            {
        //                return nextQuestion;
        //            }
        //            else
        //            {
        //                return currentQuestion;
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// GetNavigatorQuestions(int quizID, int currentQuestionID) - Retrieves the previous and next questions for the navigator
        /// </summary>
        /// <param name="quizID"></param>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public Question GetNavigatorQuestion(int quizID, int questionID, int currentQuestion)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT q.* FROM Questions q JOIN QuizQuestion qq ON q.QuestionID = qq.QuestionID WHERE qq.QuizID = @QuizID AND q.QuestionID = @QuestionID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);
                    command.Parameters.AddWithValue("@QuestionID", questionID);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Question question = new Question();
                        question.QuestionID = (int)reader["QuestionID"];
                        question.QuestionText = (string)reader["QuestionText"];
                        question.QuestionType = (string)reader["QuestionType"];

                        return question;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }


    }
}
