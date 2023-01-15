using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp.DAO;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class QuestionController : Controller
    {
        /// <summary>
        /// QuizQuestionController Container includes the methods from QuizDAO, QuestionDAO, QuizQuestionDAO AnswersDAO and CategoryDAO
        /// </summary>
        private readonly QuestionDAO _questionDAO;
        private readonly QuizDAO _quizDAO;
        private readonly CategoryDAO _categoryDAO;
        private readonly QuizQuestionDAO _quizQuestionDAO;
        private readonly ResultDAO _resultDAO;

        public QuestionController(QuestionDAO questionDAO, QuizDAO quizDAO, CategoryDAO categoryDAO, QuizQuestionDAO quizQuestionDAO, ResultDAO resultDAO)
        {
            _questionDAO = questionDAO;
            _quizDAO = quizDAO;
            _categoryDAO = categoryDAO;
            _quizQuestionDAO = quizQuestionDAO;
            _resultDAO = resultDAO;

        }


        // GET: Question/Index
        public ActionResult Index()
        {
            List<Question> questions = _questionDAO.GetAll();
            return View(questions);
        }

        // GET: Question/Details/5
        public ActionResult Details(int id)
        {
            Question question = _questionDAO.GetByID(id);
            return View(question);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question question)
        {
            try
            {
                _questionDAO.Insert(question);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //[HttpPost]
        //public IActionResult Create(Question question)
        //{
        //    _questionDAO.Create(question);
        //    return RedirectToAction("Index");
        //}

        public IActionResult Update(int id)
        {
            return View(_questionDAO.GetByID(id));
        }

        [HttpPost]
        public IActionResult Update(Question question)
        {
            _questionDAO.Update(question);
            return RedirectToAction("Index");
        }

        // GET: Question/Edit/5
        public ActionResult Edit(int id)
        {
            Question question = _questionDAO.GetByID(id);
            return View(question);
        }

        // POST: Question/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Question question)
        {
            try
            {
                _questionDAO.Update(question);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Question/Delete/5
        public ActionResult Delete(int id)
        {
            Question question = _questionDAO.GetByID(id);
            return View(question);
        }

        //public IActionResult Delete(int id)
        //{
        //    _questionDAO.Delete(id);
        //    return RedirectToAction("Index");
        //}

        // POST: Question/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Question question)
        {
            try
            {
                _questionDAO.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult GetByCategoryID(int categoryID)
        {
            return View(_questionDAO.GetByCategoryID(categoryID));
        }

        public IActionResult GetByType(string type)
        {
            return View(_questionDAO.GetByType(type));
        }

        //public IActionResult Randomize(List<Question> questions)
        //{
        //    return View(_questionDAO.Randomize(questions));
        //}

        public IActionResult Randomize(List<Question> questions)
        {
            List<Question> randomizedQuestions = _questionDAO.Randomize(questions);
            return View(randomizedQuestions);
        }

        public ActionResult GetQuestions(int quizID)
        {
            List<Question> questions = _questionDAO.GetQuestions(quizID);
            return View(questions);
        }

        public ActionResult PreviousQuestion(int quizID, int currentQuestion)
        {
            // Retrieve quiz and question information
            Quiz quiz = _quizDAO.GetByID(quizID);
            List<Question> questions = _questionDAO.GetQuestions(quizID);  //might need to revise to GetPreviousQuestion but has error of not passing currentQuestion value
            // Decrement current question index
            currentQuestion--;
            // Check if on first question
            if (currentQuestion < 0)
            {
                // Redirect to home page
                return RedirectToAction("Index", "Home");
            }
            // Return view with updated information
            return View("Quiz", new { quiz = quiz, questions = questions, currentQuestion = currentQuestion });
        }

        public ActionResult NextQuestion(int quizID, int currentQuestion)
        {
            // Retrieve next question information
            var question = _questionDAO.GetNextQuestion(quizID, currentQuestion);
            // Check if there is a next question
            if (question == null)
            {
                // Redirect to submit action
                return RedirectToAction("Submit", new { quizID = quizID });
            }

            // Update current question index
            currentQuestion++;

            // Return view
            return View("Question", new { quizID = quizID, question = question, currentQuestion = currentQuestion });

        }
        [HttpPost]
        public ActionResult MarkQuestion(int questionID, bool isMarked)
        {
            _questionDAO.MarkQuestion(questionID, isMarked);
            return RedirectToAction("Quiz");
        }

        public bool CheckIfAllQuestionsAnswered(int quizID)
        {
            // Use the QuestionDAO to check if all questions have been answered
            bool result = _questionDAO.CheckIfAllQuestionsAnswered(quizID);
            return result;
        }

        public ActionResult GetNavigatorQuestions(int quizID)
        {
            _questionDAO.GetNavigatorQuestions(quizID);
            return RedirectToAction("Quiz");
        }

        [HttpGet]
        public List<string> GetQuestionStyles(int quizID, int currentQuestion)
        {
            // Use the QuizQuestionDAO to check if the current question has been answered
            bool isAnswered = _quizQuestionDAO.IsQuestionAnswered(quizID, currentQuestion);

            // Create a list to store the styles
            List<string> styles = new List<string>();

            // Add the appropriate styles based on whether the question has been answered
            if (isAnswered)
            {
                styles.Add("answered");
            }
            else
            {
                styles.Add("unanswered");
            }

            // Return the styles
            return styles;
        }

    }
}
