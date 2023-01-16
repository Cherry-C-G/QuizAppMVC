namespace QuizApp.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public int CategoryID { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        //below created based on the extra requriment from the pdf of showing multiple choice
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string CorrectAnswer { get; set; }
        public virtual Answer Answer { get; set; }
        public string SelectedAnswer { get; internal set; }
        public string AnswerText { get; internal set; }
    }
}
