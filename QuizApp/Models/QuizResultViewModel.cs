namespace QuizApp.Models
{
    public class QuizResultViewModel
    {
        public Result Result { get; set; }
        public string QuizName { get; set; }
        public string FullName { get; set; }
        public List<QuizQuestion> QuizQuestions { get; set; }
        public bool Passed { get; set; }
        public Quiz Quiz { get; set; }
        public List<Question> Questions { get; set; }
        public List<Category> Categories { get; set; }
        public int CurrentQuestion { get; set; }
        public int TimeRemaining { get; set; }
        public List<string> QuestionStyles { get; internal set; }
        public object Answers { get; internal set; }
        public int UserID { get; internal set; }
        public string AnswerText { get; internal set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Score { get; set; }
        public string CorrectAnswerText { get; set; }
        public bool IsCorrect { get; set; }
       
    }
}