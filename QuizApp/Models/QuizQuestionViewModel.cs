namespace QuizApp.Models
{
    public class QuizQuestionViewModel
    {

            public Quiz Quiz { get; set; }
            public List<Question> Questions { get; set; }
            public List<Category> Categories { get; set; }
            public int CurrentQuestion { get; set; }
            public int TimeRemaining { get; set; }
        public List<string> QuestionStyles { get; internal set; }
        public object Answers { get; internal set; }
        public int UserID { get; internal set; }
        public object AnswerText { get; internal set; }
    }
}
