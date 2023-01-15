using Microsoft.Data.SqlClient;
using QuizApp.Models;

namespace QuizApp.DAO
{
    public class AnswerDAO
    {
        private readonly IConfiguration _configuration;
        public AnswerDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Insert(Answer answer)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Answers (QuestionID, AnswerText, IsCorrect) VALUES (@QuestionID, @AnswerText, @IsCorrect)", connection))
                {
                    command.Parameters.AddWithValue("@QuestionID", answer.QuestionID);
                    command.Parameters.AddWithValue("@AnswerText", answer.AnswerText);
                    command.Parameters.AddWithValue("@IsCorrect", answer.IsCorrect);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void SubmitAnswer(Answer answer)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Answers (QuestionID, AnswerText, IsCorrect) VALUES (@questionID, @answerText, @isCorrect)", connection);
                cmd.Parameters.AddWithValue("@questionID", answer.QuestionID);
                cmd.Parameters.AddWithValue("@answerText", answer.AnswerText);
                cmd.Parameters.AddWithValue("@isCorrect", answer.IsCorrect);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool CheckAnswer(int answerID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT IsCorrect FROM Answers WHERE AnswerID = @answerID", connection);
                command.Parameters.AddWithValue("@answerID", answerID);

                var result = command.ExecuteScalar();
                if (result != null)
                {
                    return (bool)result;
                }
                else
                {
                    return false;
                }
            }

        }



        /// <summary>
        /// Update(Answer answer) - This method can be used to update the answer details in the database.
        /// </summary>
        /// <param name="answer"></param>
        public void Update(Answer answer)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE Answers SET QuestionID = @QuestionID, AnswerText = @AnswerText, IsCorrect = @IsCorrect WHERE AnswerID = @AnswerID", connection))
                {
                    command.Parameters.AddWithValue("@AnswerID", answer.AnswerID);
                    command.Parameters.AddWithValue("@QuestionID", answer.QuestionID);
                    command.Parameters.AddWithValue("@AnswerText", answer.AnswerText);
                    command.Parameters.AddWithValue("@IsCorrect", answer.IsCorrect);

                    command.ExecuteNonQuery();
                }
            }
        }

        public Answer GetAnswer(int answerID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Answers WHERE AnswerID = @answerID", connection))
                {
                    command.Parameters.AddWithValue("@answerID", answerID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Answer()
                            {
                                AnswerID = (int)reader["AnswerID"],
                                QuestionID = (int)reader["QuestionID"],
                                AnswerText = (string)reader["AnswerText"],
                                IsCorrect = (bool)reader["IsCorrect"]
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


        public void MarkAnswer(int answerID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                // Update the answer record in the database
                var updateCommand = new SqlCommand("UPDATE Answers SET IsCorrect = 1 WHERE AnswerID = @answerID", connection);
                updateCommand.Parameters.AddWithValue("@answerID", answerID);

                updateCommand.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Delete(int id) - This method can be used to delete an answer from the database.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Answers WHERE AnswerID = @AnswerID", connection))
                {
                    command.Parameters.AddWithValue("@AnswerID", id);

                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Read answer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Answer GetByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Answers WHERE AnswerID = @AnswerID", connection))
                {
                    command.Parameters.AddWithValue("@AnswerID", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Answer answer = new Answer();
                        answer.AnswerID = (int)reader["AnswerID"];
                        answer.QuestionID = (int)reader["QuestionID"];
                        answer.AnswerText = (string)reader["AnswerText"];
                        answer.IsCorrect = (bool)reader["IsCorrect"];

                        return answer;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// GetByQuestionID(int questionID) - This method can be used to get all answers of a specific question.
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public List<Answer> GetByQuestionID(int questionID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Answers WHERE QuestionID = @QuestionID", connection))
                {
                    command.Parameters.AddWithValue("@QuestionID", questionID);

                    SqlDataReader reader = command.ExecuteReader();

                    List<Answer> answers = new List<Answer>();

                    while (reader.Read())
                    {
                        Answer answer = new Answer();
                        answer.AnswerID = (int)reader["AnswerID"];
                        answer.QuestionID = (int)reader["QuestionID"];
                        answer.AnswerText = (string)reader["AnswerText"];
                        answer.IsCorrect = (bool)reader["IsCorrect"];

                        answers.Add(answer);
                    }
                    return answers;
                }
            }
        }
        /// <summary>
        /// GetAll() - This method can be used to get all answers from the database.
        /// </summary>
        /// <returns></returns>
        public List<Answer> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Answers", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<Answer> answers = new List<Answer>();

                    while (reader.Read())
                    {
                        Answer answer = new Answer();
                        answer.AnswerID = (int)reader["AnswerID"];
                        answer.QuestionID = (int)reader["QuestionID"];
                        answer.AnswerText = (string)reader["AnswerText"];
                        answer.IsCorrect = (bool)reader["IsCorrect"];
                        answers.Add(answer);
                    }

                    return answers;
                }
            }
        }
        /// <summary>
        /// GetCorrectAnswer(int questionID) - This method can be used to get the correct answer of a specific question.
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public Answer GetCorrectAnswer(int questionID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Answers WHERE QuestionID = @QuestionID AND IsCorrect = 1", connection))
                {
                    command.Parameters.AddWithValue("@QuestionID", questionID);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Answer answer = new Answer();
                        answer.AnswerID = (int)reader["AnswerID"];
                        answer.QuestionID = (int)reader["QuestionID"];
                        answer.AnswerText = (string)reader["AnswerText"];
                        answer.IsCorrect = (bool)reader["IsCorrect"];

                        return answer;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// IsAnswerExist(string answer, int questionID) - This method can be used to check if an answer already exists for a specific question.
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public bool IsAnswerExist(string answer, int questionID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Answers WHERE AnswerText = @AnswerText AND QuestionID = @QuestionID", connection))
                {
                    command.Parameters.AddWithValue("@AnswerText", answer);
                    command.Parameters.AddWithValue("@QuestionID", questionID);

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
        /// RandomizeAnswers(List<Answer> answers) - This method can be used to randomize a list of answers.
        /// </summary>
        /// <param name="answers"></param>
        public void RandomizeAnswers(List<Answer> answers)
        {
            Random rng = new Random();
            int n = answers.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Answer value = answers[k];
                answers[k] = answers[n];
                answers[n] = value;
            }
        }
        /// <summary>
        /// GetCount() - This method can be used to get the total number of answers in the database.
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Answers", connection))
                {
                    return (int)command.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// GetByQuizQuestionID(int QuizQuestionID) - This method can be used to get all answers by QuizQuestionID.
        /// </summary>
        /// <param name="QuizQuestionID"></param>
        /// <returns></returns>
        public List<Answer> GetByQuizQuestionID(int QuizQuestionID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT a.* FROM Answers a JOIN QuizQuestion qq ON a.AnswerID = qq.AnswerID WHERE qq.QuizQuestionID = @QuizQuestionID", connection))
                {
                    command.Parameters.AddWithValue("@QuizQuestionID", QuizQuestionID);

                    SqlDataReader reader = command.ExecuteReader();
                    List<Answer> answers = new List<Answer>();

                    while (reader.Read())
                    {
                        Answer answer = new Answer();
                        answer.AnswerID = (int)reader["AnswerID"];
                        answer.QuestionID = (int)reader["QuestionID"];
                        answer.AnswerText = (string)reader["AnswerText"];
                        answer.IsCorrect = (bool)reader["IsCorrect"];

                        answers.Add(answer);
                    }

                    return answers;
                }
            }
        }


    }
}
