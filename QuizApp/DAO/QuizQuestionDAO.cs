using Microsoft.Data.SqlClient;
using QuizApp.Models;

namespace QuizApp.DAO
{
    public class QuizQuestionDAO
    {
        private readonly IConfiguration _configuration;
        public QuizQuestionDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Create(QuizQuestion quizQuestion)
        /// </summary>
        /// <param name="quizID"></param>
        /// <param name="questionID"></param>
        /// <param name="answerID"></param>
        public void Create(int quizID, int questionID, int answerID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO QuizQuestion (QuizID, QuestionID, AnswerID) VALUES (@QuizID, @QuestionID, @AnswerID)", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);
                    command.Parameters.AddWithValue("@QuestionID", questionID);
                    command.Parameters.AddWithValue("@AnswerID", answerID);
                    command.ExecuteNonQuery();
                }
            }
        }


        //public QuizQuestionDAO GetByID(int id)
        //{
        //    using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        connection.Open();

        //        using (SqlCommand command = new SqlCommand("SELECT * FROM QuizQuestion WHERE QuizQuestionID = @QuizQuestionID", connection))
        //        {
        //            command.Parameters.AddWithValue("@QuizQuestionID", id);

        //            SqlDataReader reader = command.ExecuteReader();

        //            if (reader.Read())
        //            {
        //                QuizQuestion quizQuestion = new QuizQuestion();
        //                quizQuestion.QuizQuestionID = (int)reader["QuizQuestionID"];
        //                quizQuestion.QuizID = (int)reader["QuizID"];
        //                quizQuestion.QuestionID = (int)reader["QuestionID"];
        //                quizQuestion.AnswerID = (int)reader["AnswerID"];
        //                return quizQuestion;
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>
        public List<QuizQuestion> GetByQuizID(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM QuizQuestions WHERE QuizID = @QuizID", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);

                    SqlDataReader reader = command.ExecuteReader();

                    List<QuizQuestion> quizQuestions = new List<QuizQuestion>();

                    while (reader.Read())
                    {
                        QuizQuestion quizQuestion = new QuizQuestion();
                        quizQuestion.QuizQuestionID = (int)reader["QuizQuestionID"];
                        quizQuestion.QuizID = (int)reader["QuizID"];
                        quizQuestion.QuestionID = (int)reader["QuestionID"];
                        quizQuestion.AnswerID = (int)reader["AnswerID"];
                        quizQuestions.Add(quizQuestion);
                    }

                    return quizQuestions;
                }
            }
        }



        /// <summary>
        /// GetByQuestionID(int questionID)
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public List<QuizQuestion> GetByQuestionID(int questionID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM QuizQuestions WHERE QuestionID = @QuestionID", connection))
                {
                    command.Parameters.AddWithValue("@QuestionID", questionID);

                    SqlDataReader reader = command.ExecuteReader();

                    List<QuizQuestion> quizQuestions = new List<QuizQuestion>();
                    while (reader.Read())
                    {
                        QuizQuestion quizQuestion = new QuizQuestion();
                        quizQuestion.QuizQuestionID = (int)reader["QuizQuestionID"];
                        quizQuestion.QuizID = (int)reader["QuizID"];
                        quizQuestion.QuestionID = (int)reader["QuestionID"];
                        quizQuestion.AnswerID = (int)reader["AnswerID"];
                        quizQuestions.Add(quizQuestion);
                    }
                    return quizQuestions;
                }
            }
        }

        public List<QuizQuestion> GetQuizQuestionsByResult(int resultID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM QuizQuestions WHERE ResultID = @resultID", connection))
                {
                    command.Parameters.AddWithValue("@resultID", resultID);
                    List<QuizQuestion> quizQuestionList = new List<QuizQuestion>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            quizQuestionList.Add(new QuizQuestion()
                            {
                                QuizQuestionID = (int)reader["QuizQuestionID"],
                                QuizID = (int)reader["QuizID"],
                                QuestionID = (int)reader["QuestionID"],
                                UserID = (int)reader["UserID"],
                                AnswerID = (int)reader["AnswerID"],
                                IsCorrect = (bool)reader["IsCorrect"]
                            });
                        }
                    }
                    return quizQuestionList;
                }
            }
        }




        /// <summary>
        /// GetByAnswerID(int answerID)
        /// </summary>
        /// <param name="answerID"></param>
        /// <returns></returns>
        public List<QuizQuestion> GetByAnswerID(int answerID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM QuizQuestions WHERE AnswerID = @AnswerID", connection))
                {
                    command.Parameters.AddWithValue("@AnswerID", answerID);

                    SqlDataReader reader = command.ExecuteReader();

                    List<QuizQuestion> quizQuestions = new List<QuizQuestion>();
                    while (reader.Read())
                    {
                        QuizQuestion quizQuestion = new QuizQuestion();
                        quizQuestion.QuizQuestionID = (int)reader["QuizQuestionID"];
                        quizQuestion.QuizID = (int)reader["QuizID"];
                        quizQuestion.QuestionID = (int)reader["QuestionID"];
                        quizQuestion.AnswerID = (int)reader["AnswerID"];
                        quizQuestions.Add(quizQuestion);
                    }
                    return quizQuestions;
                }
            }
        }

        public List<QuizQuestion> GetAnswers(int quizID, int userID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM QuizQuestions WHERE QuizID = @quizID AND UserID = @userID", connection))
                {
                    command.Parameters.AddWithValue("@quizID", quizID);
                    command.Parameters.AddWithValue("@userID", userID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<QuizQuestion> quizQuestions = new List<QuizQuestion>();
                        while (reader.Read())
                        {
                            quizQuestions.Add(new QuizQuestion
                            {
                                QuizQuestionID = (int)reader["QuizQuestionID"],
                                QuizID = (int)reader["QuizID"],
                                QuestionID = (int)reader["QuestionID"],
                                UserID = (int)reader["UserID"],
                                AnswerID = (int)reader["AnswerID"],
                                AnsweredCorrectly = (bool)reader["AnsweredCorrectly"]
                            });
                        }
                        return quizQuestions;
                    }
                }
            }
        }


        /// <summary>
        /// Update(QuizQuestion quizQuestion)
        /// </summary>
        /// <param name="quizQuestion"></param>
        public void Update(QuizQuestion quizQuestion)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE QuizQuestion SET QuizID = @QuizID, QuestionID = @QuestionID, AnswerID = @AnswerID WHERE QuizQuestionID = @QuizQuestionID", connection))
                {
                    command.Parameters.AddWithValue("@QuizQuestionID", quizQuestion.QuizQuestionID);
                    command.Parameters.AddWithValue("@QuizID", quizQuestion.QuizID);
                    command.Parameters.AddWithValue("@QuestionID", quizQuestion.QuestionID);
                    command.Parameters.AddWithValue("@AnswerID", quizQuestion.AnswerID);

                    command.ExecuteNonQuery();
                }
            }
        }

        public bool IsQuestionAnswered(int quizID, int currentQuestion)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM QuizQuestions WHERE QuizID = @quizID AND QuestionNumber = @currentQuestion AND AnswerText IS NOT NULL", connection);
                command.Parameters.AddWithValue("@quizID", quizID);
                command.Parameters.AddWithValue("@currentQuestion", currentQuestion);

                int result = (int)command.ExecuteScalar();

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

            /// <summary>
            /// Delete(int id)
            /// </summary>
            /// <param name="id"></param>
            public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM QuizQuestion WHERE QuizQuestionID = @QuizQuestionID", connection))
                {
                    command.Parameters.AddWithValue("@QuizQuestionID", id);

                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// GetAll()
        /// </summary>
        /// <returns></returns>
        public List<QuizQuestion> GetAll()
        {
            List<QuizQuestion> quizQuestions = new List<QuizQuestion>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM QuizQuestion", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        QuizQuestion quizQuestion = new QuizQuestion();
                        quizQuestion.QuizQuestionID = (int)reader["QuizQuestionID"];
                        quizQuestion.QuizID = (int)reader["QuizID"];
                        quizQuestion.QuestionID = (int)reader["QuestionID"];
                        quizQuestion.AnswerID = (int)reader["AnswerID"];
                        quizQuestions.Add(quizQuestion);
                    }
                }
            }

            return quizQuestions;
        }
        /// <summary>
        /// GetRandomQuestions(int quizID, int numberOfQuestions)
        /// </summary>
        /// <param name="quizID">no quizID in question table anymore</param>
        /// <param name="numberOfQuestions"></param>
        /// <returns></returns>
        public List<Question> GetRandomQuestions(int quizID, int numberOfQuestions)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT TOP (@NumberOfQuestions) q.* FROM Questions q JOIN QuizQuestion qq ON q.QuestionID = qq.QuestionID  ORDER BY NEWID()", connection))
                {

                    command.Parameters.AddWithValue("@NumberOfQuestions", numberOfQuestions);

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
        /// <summary>
        /// GetQuestionsByCategory(int quizID, int categoryID)
        /// </summary>
        /// <param name="quizID"></param>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<Question> GetQuestionsByCategory(int quizID, int categoryID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT q.* FROM Questions q JOIN QuizQuestion qq ON q.QuestionID = qq.QuestionID WHERE  q.CategoryID = @CategoryID", connection))
                {

                    command.Parameters.AddWithValue("@CategoryID", categoryID);

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

        public void SaveAnswer(int quizID, int questionID, int answerID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO QuizQuestions (QuizID, QuestionID, AnswerID) VALUES (@QuizID, @QuestionID, @AnswerID)", connection);
                command.Parameters.AddWithValue("@QuizID", quizID);
                command.Parameters.AddWithValue("@QuestionID", questionID);
                command.Parameters.AddWithValue("@AnswerID", answerID);
                command.ExecuteNonQuery();
            }
        }

        public int GetCorrectAnswerID(int questionID)
        {
            int correctAnswerID = 0;
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT AnswerID FROM Answers WHERE QuestionID = @QuestionID AND IsCorrect = 1", connection);
                command.Parameters.AddWithValue("@QuestionID", questionID);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    correctAnswerID = (int)reader["AnswerID"];
                }
            }
            return correctAnswerID;
        }

        public bool IsAnswerCorrect(int quizID, int questionID, int answerID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM QuizQuestion WHERE QuizID = @quizID AND QuestionID = @questionID AND AnswerID = @answerID AND AnswerID IN (SELECT AnswerID FROM Answer WHERE IsCorrect = 1)", connection))
                {
                    command.Parameters.AddWithValue("@quizID", quizID);
                    command.Parameters.AddWithValue("@questionID", questionID);
                    command.Parameters.AddWithValue("@answerID", answerID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(0) > 0;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// GetCorrectAnswers(int quizID)
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>
        public List<Answer> GetCorrectAnswers(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT a.* FROM Answers a JOIN QuizQuestion qq ON a.AnswerID = qq.AnswerID JOIN Questions q ON qq.QuestionID = q.QuestionID WHERE qq.QuizID = @QuizID AND a.IsCorrect = 1", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);
                    SqlDataReader reader = command.ExecuteReader();

                    List<Answer> correctAnswers = new List<Answer>();
                    while (reader.Read())
                    {
                        Answer answer = new Answer();
                        answer.AnswerID = (int)reader["AnswerID"];
                        answer.QuestionID = (int)reader["QuestionID"];
                        answer.AnswerText = (string)reader["AnswerText"];
                        answer.IsCorrect = (bool)reader["IsCorrect"];
                        correctAnswers.Add(answer);
                    }
                    return correctAnswers;
                }
            }
        }
        /// <summary>
        /// GetIncorrectAnswers(int quizID)
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>
        public List<Answer> GetIncorrectAnswers(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT a.* FROM Answers a JOIN QuizQuestion qq ON a.AnswerID = qq.AnswerID JOIN Questions q ON qq.QuestionID = q.QuestionID WHERE qq.QuizID = @QuizID AND a.IsCorrect = 0", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);

                    SqlDataReader reader = command.ExecuteReader();

                    List<Answer> incorrectAnswers = new List<Answer>();

                    while (reader.Read())
                    {
                        incorrectAnswers.Add(new Answer
                        {
                            AnswerID = (int)reader["AnswerID"],
                            QuestionID = (int)reader["QuestionID"],
                            AnswerText = (string)reader["AnswerText"],
                            IsCorrect = (bool)reader["IsCorrect"]
                        });
                    }

                    return incorrectAnswers;
                }
            }
        }
        /// <summary>
        /// GetUnansweredQuestions(int quizID)
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>
        public List<Question> GetUnansweredQuestions(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT q.* FROM Questions q WHERE q.QuestionID NOT IN (SELECT qq.QuestionID FROM QuizQuestion )", connection))
                {


                    SqlDataReader reader = command.ExecuteReader();

                    List<Question> unansweredQuestions = new List<Question>();

                    while (reader.Read())
                    {
                        unansweredQuestions.Add(new Question
                        {
                            QuestionID = (int)reader["QuestionID"],
                            CategoryID = (int)reader["CategoryID"],
                            QuestionText = (string)reader["QuestionText"],
                            QuestionType = (string)reader["QuestionType"]
                        });
                    }

                    return unansweredQuestions;
                }
            }
        }
        /// <summary>
        /// GetQuizScore(int quizID)
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>
        public int GetQuizScore(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM QuizQuestion qq JOIN Answers a ON qq.AnswerID = a.AnswerID WHERE qq.QuizID = @QuizID AND a.IsCorrect = 1", connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizID);

                    return (int)command.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// GetQuizQuestionByID(int QuizQuestionID)
        /// </summary>
        /// <param name="QuizQuestionID"></param>
        /// <returns></returns>
        public QuizQuestion GetQuizQuestionByID(int QuizQuestionID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM QuizQuestion WHERE QuizQuestionID = @QuizQuestionID", connection))
                {
                    command.Parameters.AddWithValue("@QuizQuestionID", QuizQuestionID);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        QuizQuestion quizQuestion = new QuizQuestion();
                        quizQuestion.QuizQuestionID = (int)reader["QuizQuestionID"];
                        quizQuestion.QuizID = (int)reader["QuizID"];
                        quizQuestion.QuestionID = (int)reader["QuestionID"];
                        quizQuestion.AnswerID = (int)reader["AnswerID"];

                        return quizQuestion;
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
