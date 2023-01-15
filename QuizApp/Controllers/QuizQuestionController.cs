using Microsoft.AspNetCore.Mvc;
using QuizApp.DAO;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class QuizQuestionController : Controller
    {
        private readonly QuizDAO _quizDAO;
        private readonly QuestionDAO _questionDAO;
        private readonly QuizQuestionDAO _quizQuestionDAO;
        private readonly CategoryDAO _categoryDAO;

        public QuizQuestionController(QuizDAO quizDAO, QuestionDAO questionDAO, QuizQuestionDAO quizQuestionDAO, CategoryDAO categoryDAO)
        {
            _quizDAO = quizDAO;
            _questionDAO = questionDAO;
            _quizQuestionDAO = quizQuestionDAO;
            _categoryDAO = categoryDAO;
        }

        // GET: QuizQuestion/Start
        public ActionResult Start(int quizID, int userID)
        {
            // Retrieve quiz and question information
            Quiz quiz = _quizDAO.GetByID(quizID);
            List<Question> questions = _questionDAO.GetRandomQuestions(quizID, 10);
            List<Category> categories = _categoryDAO.GetAll();

            // Set up view model
            QuizQuestionViewModel viewModel = new QuizQuestionViewModel
            {
                Quiz = quiz,
                Questions = questions,
                Categories = categories,
                CurrentQuestion = 0,
                TimeRemaining = _quizDAO.GetTimeRemaining(quizID, userID)
            };

            // Return view
            return View(viewModel);
        }

        // POST: QuizQuestion/Next
        [HttpPost]
        public ActionResult Next(int quizID, int currentQuestion)
        {
            // Retrieve quiz and question information
            Quiz quiz = _quizDAO.GetByID(quizID);
            List<Question> questions = _questionDAO.GetRandomQuestions(quizID, 10);
            List<Category> categories = _categoryDAO.GetAll();
            // Update current question index
            currentQuestion++;

            // Check if on last question
            if (currentQuestion == questions.Count)
            {
                // Redirect to submit action
                return RedirectToAction("Submit", new { quizID = quizID, currentQuestion = currentQuestion });
            }

            // Check if the user has exceeded the time limit
            if (quiz.TimeLimit <= 0)
            {
                // Show alert and force submit
                TempData["timeExceeded"] = true;
                return RedirectToAction("Submit", new { quizID = quizID, currentQuestion = currentQuestion });
            }    
            
            // Retrieve updated question styles
            List<string> questionStyles = _quizDAO.GetQuestionStyles(quizID, currentQuestion);

            // Return view
            return View("Start", new { quiz = quiz, questions = questions, categories = categories, currentQuestion = currentQuestion, questionStyles = questionStyles });
        }

        // POST: QuizQuestion/Previous
        [HttpPost]
        public ActionResult Previous(int quizID, int currentQuestion)
        {
            // Retrieve quiz and question information
            Quiz quiz = _quizDAO.GetByID(quizID);
            List<Question> questions = _questionDAO.GetRandomQuestions(quizID, 10);
            List<Category> categories = _categoryDAO.GetAll();
            // Update current question index
            currentQuestion--;

            // Check if on first question
            if (currentQuestion < 0)
            {
                currentQuestion = 0;
            }

            // Retrieve updated question styles
            List<string> questionStyles = _quizDAO.GetQuestionStyles(quizID, currentQuestion);

            // Return view
            return View("Start", new { quiz = quiz, questions = questions, categories = categories, currentQuestion = currentQuestion, questionStyles = questionStyles });
        }

        // POST: QuizQuestion/Submit
        [HttpPost]
        public ActionResult Submit(int quizID,int userID, int currentQuestion)
        {
            // Retrieve quiz and question information
            Quiz quiz = _quizDAO.GetByID(quizID);
            List<Question> questions = _questionDAO.GetRandomQuestions(quizID, 10);
            List<Category> categories = _categoryDAO.GetAll();
            // Check if all questions have been answered
            if (!_quizDAO.CheckIfAllQuestionsAnswered(quizID,userID))
            {
                TempData["notAllAnswered"] = true;
                return View("Start", new { quiz = quiz, questions = questions, categories = categories, currentQuestion = currentQuestion });
            }

            // Retrieve time remaining
            int timeRemaining = _quizDAO.GetTimeRemaining(quizID, userID);
            if (timeRemaining <= 0)
            {
                TempData["timeExceeded"] = true;
                return RedirectToAction("Submit", new { quizID = quizID, currentQuestion = currentQuestion });
            }
            else
            {
                return View("Start", new { quiz = quiz, questions = questions, categories = categories, currentQuestion = currentQuestion, timeRemaining = timeRemaining });
            }
        }

        //// POST: QuizQuestion/Previous
        //[HttpPost]
        //public ActionResult Previous(int quizID, int currentQuestion)
        //{
        //    // Retrieve quiz and question information
        //    Quiz quiz = _quizDAO.GetByID(quizID);
        //    List<Question> questions = _questionDAO.GetRandomQuestions(quizID, 10);
        //    List<Category> categories = _categoryDAO.GetAll();
        //    // Update current question index
        //    currentQuestion--;
        //    return View("Start", new { quiz = quiz, questions = questions, categories = categories, currentQuestion = currentQuestion });
        //}

        // POST: QuizQuestion/Submit
        [HttpPost]
        public ActionResult Submit(int quizID, int currentQuestion)
        {
            // Retrieve quiz and question information
            Quiz quiz = _quizDAO.GetByID(quizID);
            _quizDAO.SubmitQuiz(quizID);
            return RedirectToAction("QuizResult", "Quiz", new { quizID = quizID });
        }
    }
}
