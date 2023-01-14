namespace QuizApp.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; }
        public int UserID { get; set; }
        public int Rating { get; set; }
        public string FeedbackText { get; set; }

    }
}
