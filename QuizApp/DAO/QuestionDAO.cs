using Microsoft.Data.SqlClient;
using QuizApp.Models;

namespace QuizApp.DAO
{

    public class QuestionDAO
    {
        private readonly IConfiguration _configuration;
        private readonly QuizDAO _quizDAO;
        public QuestionDAO(IConfiguration configuration, QuizDAO quizDAO)
        {
            _configuration = configuration;
            _quizDAO = quizDAO;
        }


        public void Create(Question question)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Questions ( CategoryID, QuestionText, QuestionType) VALUES ( @CategoryID, @QuestionText, @QuestionType)", connection))
                {

                    command.Parameters.AddWithValue("@CategoryID", question.CategoryID);
                    command.Parameters.AddWithValue("@QuestionText", question.QuestionText);
                    command.Parameters.AddWithValue("@QuestionType", question.QuestionType);
                    command.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Delete(int id) - Deletes a specific question by its ID
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Questions WHERE QuestionID = @QuestionID", connection))
                {
                    command.Parameters.AddWithValue("@QuestionID", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Answer> GetAnswers(int questionID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Answers WHERE QuestionID = @QuestionID", connection);
                command.Parameters.AddWithValue("@QuestionID", questionID);
                SqlDataReader reader = command.ExecuteReader();
                List<Answer> answers = new List<Answer>();
                while (reader.Read())
                {
                    Answer answer = new Answer
                    {
                        AnswerID = (int)reader["AnswerID"],
                        QuestionID = (int)reader["QuestionID"],
                        AnswerText = (string)reader["AnswerText"],
                        IsCorrect = (bool)reader["IsCorrect"]
                    };
                    answers.Add(answer);
                }
                return answers;
            }
        }

        public IEnumerable<Question> GetByQuizID(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Questions WHERE QuizID = @quizID", connection))
                {
                    command.Parameters.AddWithValue("@quizID", quizID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Question> questions = new List<Question>();
                        while (reader.Read())
                        {
                            questions.Add(new Question
                            {
                                QuestionID = (int)reader["QuestionID"],
                                QuestionText = (string)reader["QuestionText"],
                                QuestionType = (string)reader["QuestionType"]
                            });
                        }
                        return questions;
                    }
                }
            }
        }

        /// <summary>
        /// GetAll() - Retrieves all questions
        /// </summary>
        /// <returns></returns>
        public List<Question> GetAll()
        {
            List<Question> questions = new List<Question>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Questions", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Question question = new Question
                            {
                                QuestionID = (int)reader["QuestionID"],
                                CategoryID = (int)reader["CategoryID"],
                                QuestionText = (string)reader["QuestionText"],
                                QuestionType = (string)reader["QuestionType"]
                            };
                            questions.Add(question);
                        }
                    }
                }
                return questions;
            }
        }

        //}
        /// <summary>
        /// GetByID(int id) - Retrieves a specific question by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Question GetByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Questions WHERE QuestionID = @QuestionID", connection))
                {
                    command.Parameters.AddWithValue("@QuestionID", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Question
                            {
                                QuestionID = (int)reader["QuestionID"],
                                CategoryID = (int)reader["CategoryID"],
                                QuestionText = (string)reader["QuestionText"],
                                QuestionType = (string)reader["QuestionType"]
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

        public List<Question> GetQuestions(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Question ", connection))
                {
                    command.Parameters.AddWithValue("@quizID", quizID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Question> questionList = new List<Question>();
                        while (reader.Read())
                        {
                            questionList.Add(new Question()
                            {
                                QuestionID = (int)reader["QuestionID"],
                                QuestionText = (string)reader["QuestionText"],
                                QuestionType = (string)reader["QuestionType"]
                            });
                        }
                        return questionList;
                    }
                }
            }
        }

        public List<Question> GetByQuizIDAndUserID(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT q.QuestionID, q.QuestionText, q.QuizID, a.AnswerID, a.AnswerText, a.IsCorrect
                                        FROM Question q 
                                        JOIN Answer a on q.AnswerID = a.AnswerID
                                        WHERE q.QuizID = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();
                    var questions = new List<Question>();
                    while (reader.Read())
                    {
                        questions.Add(new Question()
                        {
                            QuestionID = (int)reader["QuestionID"],
                            QuestionText = (string)reader["QuestionText"],
                            Answer = new Answer
                            {
                                AnswerID = (int)reader["AnswerID"],
                                AnswerText = (string)reader["AnswerText"],
                                IsCorrect = (bool)reader["IsCorrect"],
                            }
                        });
                    }
                    return questions;
                }
            }
        }



        public Question GetNextQuestion(int quizID, int currentQuestion)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Questions WHERE QuizID = @quizID AND QuestionID = @currentQuestion + 1", connection);
                command.Parameters.AddWithValue("@quizID", quizID);
                command.Parameters.AddWithValue("@currentQuestion", currentQuestion);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Question
                        {
                            QuestionID = (int)reader["QuestionID"],
                            QuestionText = (string)reader["QuestionText"],
                            QuestionType = (string)reader["QuestionType"]
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public void MarkQuestion(int questionID, bool isMarked)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("update question set isMarked = @isMarked where questionID = @questionID", connection);
                cmd.Parameters.AddWithValue("@questionID", questionID);
                cmd.Parameters.AddWithValue("@isMarked", isMarked);
                cmd.ExecuteNonQuery();
            }
        }

        public bool CheckIfAllQuestionsAnswered(int quizID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                // Check if all questions have been answered
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM QuizQuestions WHERE QuizID = @quizID AND AnswerID IS NULL", connection);
                cmd.Parameters.AddWithValue("@quizID", quizID);
                connection.Open();
                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public List<Question> GetNavigatorQuestions(int quizID)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT QuestionID, QuestionText FROM Questions WHERE QuizID = @quizID", connection))
                {
                    command.Parameters.AddWithValue("@quizID", quizID);
                    var reader = command.ExecuteReader();
                    var questions = new List<Question>();
                    while (reader.Read())
                    {
                        questions.Add(new Question
                        {
                            QuestionID = (int)reader["QuestionID"],
                            QuestionText = (string)reader["QuestionText"]
                        });
                    }
                    return questions;
                }
            }
        }



        public Question GetPreviousQuestions(int quizID, int currentQuestion)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Questions WHERE QuizID = @quizID AND QuestionID < @currentQuestion ORDER BY QuestionID DESC", connection);
                command.Parameters.AddWithValue("@quizID", quizID);
                command.Parameters.AddWithValue("@currentQuestion", currentQuestion);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Question
                    {
                        QuestionID = (int)reader["QuestionID"],
                        QuestionText = (string)reader["QuestionText"],
                        QuestionType = (string)reader["QuestionType"]
                    };
                }
                else
                {
                    return null;
                }
            }
        }



        /// <summary>
        /// Update(Question question) - Updates an existing question in the database
        /// </summary>
        /// <param name="question"></param>
        public void Update(Question question)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Questions SET  CategoryID = @CategoryID, QuestionText = @QuestionText, QuestionType = @QuestionType WHERE QuestionID = @QuestionID", connection))
                {

                    command.Parameters.AddWithValue("@CategoryID", question.CategoryID);
                    command.Parameters.AddWithValue("@QuestionText", question.QuestionText);
                    command.Parameters.AddWithValue("@QuestionType", question.QuestionType);
                    command.Parameters.AddWithValue("@QuestionID", question.QuestionID);

                    command.ExecuteNonQuery();
                }
            }
        }

        //public void Delete(int id)
        //{
        //    using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        connection.Open();

        //        using (SqlCommand command = new SqlCommand("DELETE FROM Questions WHERE QuestionID = @QuestionID", connection))
        //        {
        //            command.Parameters.AddWithValue("@QuestionID", id);

        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}


        /// <summary>
        /// GetByCategoryID(int categoryID) - Retrieves all questions for a specific category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<Question> GetByCategoryID(int categoryID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Questions WHERE CategoryID = @CategoryID", connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", categoryID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
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

        public List<Question> GetByType(string type)
        {
            List<Question> questions = new List<Question>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Questions WHERE QuestionType = @QuestionType", connection))
                {
                    command.Parameters.AddWithValue("@QuestionType", type);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Question question = new Question();
                            question.QuestionID = (int)reader["QuestionID"];
                            question.CategoryID = (int)reader["CategoryID"];
                            question.QuestionText = (string)reader["QuestionText"];
                            question.QuestionType = (string)reader["QuestionType"];
                            questions.Add(question);
                        }
                    }
                }
            }

            return questions;
        }

        //public void Randomize(List<Question> questions)
        //{
        //    Random rng = new Random();
        //    int n = questions.Count;
        //    while (n > 1)
        //    {
        //        n--;
        //        int k = rng.Next(n + 1);
        //        Question value = questions[k];
        //        questions[k] = questions[n];
        //        questions[n] = value;
        //    }
        //}

        /// <summary>
        /// GetRandomQuestions(int categoryID, int numberOfQuestions) - Retrieves a random set of questions for a specific category and number of questions
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
        public List<Question> Randomize(List<Question> questions)
        {
            Random rnd = new Random();
            return questions.OrderBy(x => rnd.Next()).ToList();
        }

        public List<Question> GetRandomQuestions(int numberOfQuestions, int categoryID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT TOP (@NumberOfQuestions) * FROM Questions WHERE CategoryID = @CategoryID ORDER BY NEWID()", connection))
                {
                    command.Parameters.AddWithValue("@NumberOfQuestions", numberOfQuestions);
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

        /// <summary>
        /// Insert(Question question) - Inserts a new question into the database
        /// </summary>
        /// <param name="question"></param>
        public void Insert(Question question)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Questions (CategoryID, QuestionText, QuestionType) VALUES (@CategoryID, @QuestionText, @QuestionType)", connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", question.CategoryID);
                    command.Parameters.AddWithValue("@QuestionText", question.QuestionText);
                    command.Parameters.AddWithValue("@QuestionType", question.QuestionType);
                    command.ExecuteNonQuery();
                }
            }

        }
        /// <summary>
        /// Search(string keyword, string questionType) - Retrieves questions based on a keyword and/or question type
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<Question> Search(string keyword)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Questions WHERE QuestionText LIKE @Keyword OR CategoryID IN (SELECT CategoryID FROM Categories WHERE CategoryName LIKE @Keyword)", connection))
                {
                    command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

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
        /// GetCount() - Retrieves the total number of questions in the database
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Questions", connection))
                {
                    return (int)command.ExecuteScalar();
                }
            }

        }
        /// <summary>
        /// GetQuestionTypes() - Retrieves a list of all available question types.
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public string GetQuestionType(int questionID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT QuestionType FROM Questions WHERE QuestionID = @QuestionID", connection))
                {
                    command.Parameters.AddWithValue("@QuestionID", questionID);

                    return (string)command.ExecuteScalar();
                }
            }
        }

    }
}
