namespace QuizApp.Models
{
    public class QuizQuestion
    {
        public int QuizQuestionID { get; set; }
        public int QuizID { get; set; }
        public int QuestionID { get; set; }
        public int AnswerID { get; set; }
    }
}
