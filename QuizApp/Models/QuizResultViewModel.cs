namespace QuizApp.Models
{
    public class QuizResultViewModel
    {
        public Result Result { get; set; }
        public string QuizName { get; set; }
        public string UserFullName { get; set; }
        public List<QuizQuestion> QuizQuestions { get; set; }
        public bool Passed { get; set; }
    }
}