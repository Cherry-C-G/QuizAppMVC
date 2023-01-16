namespace QuizApp.Models
{
    public class QuizQuestion
    {
        public int QuizQuestionID { get; set; }
        public int QuizID { get; set; }
        public int QuestionID { get; set; }
        public int AnswerID { get; set; }
        //below properties added based on the more implementation in the DAO and Controller class
        public bool IsCorrect { get; internal set; }
        public bool AnsweredCorrectly { get; internal set; }
        public int UserID { get; internal set; }
        public int ResultID { get; internal set; }
        public string AnswerText { get; internal set; }
    }
}
