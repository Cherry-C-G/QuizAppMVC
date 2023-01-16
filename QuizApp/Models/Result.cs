namespace QuizApp.Models
{
    public class Result
    {
        public int ResultID { get; set; }
        public int UserID { get; set; }
        public int QuizID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Score { get; set; }
        public List<Question> Questions { get; set; }
    }
}
