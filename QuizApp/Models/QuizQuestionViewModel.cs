namespace QuizApp.Models
{
    public class QuizQuestionViewModel
    {

            public Quiz Quiz { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
        public Category Category { get; set; }
            public List<Question> Questions { get; set; }
            public List<Category> Categories { get; set; }
            public int CurrentQuestion { get; set; }
            public int TimeRemaining { get; set; }
        public List<string> QuestionStyles { get; internal set; }
        public List<List<Answer>> Answers { get; set; }
        public int UserID { get; internal set; }
        public object AnswerText { get; internal set; }
        public string QuestionText { get; set; }
        public bool IsCorrect { get; set; }
 
    }


}
