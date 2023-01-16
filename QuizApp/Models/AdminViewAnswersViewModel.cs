namespace QuizApp.Models
{
    internal class AdminViewAnswersViewModel
    {
        public Users Users { get; internal set; }
        public List<Quiz> Quizzes { get; internal set; }
        public List<Question> Questions { get; internal set; }
        public List<Answer> Answers { get; internal set; }
    }
}