using Microsoft.AspNetCore.Mvc;
using QuizApp.DAO;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class ResultController : Controller
    {
        /// <summary>
        /// ResultController contains the methods from  ResultDAO
        /// </summary>
        /// <returns></returns>
        /// 
        private readonly ResultDAO _resultDAO;
        private readonly QuizDAO _quizDAO;
        private readonly UsersDAO _usersDAO;
        private readonly QuizQuestionDAO _quizQuestionDAO;
        private readonly QuestionDAO _questionDAO;
        public ResultController(ResultDAO resultDAO, QuizDAO quizDAO, UsersDAO usersDAO, QuizQuestionDAO quizQuestionDAO, QuestionDAO questionDAO)
        {
            _resultDAO = resultDAO;
            _quizDAO = quizDAO;
            _usersDAO = usersDAO;
            _quizQuestionDAO = quizQuestionDAO;
            _questionDAO = questionDAO;
        }
        /// <summary>
        /// DeleteResult(int resultID): This method would be responsible for deleting a specific result from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET: Result/Details/5
        public ActionResult Details(int id)
        {
            Result result = _resultDAO.GetResult(id);
            return View(result);
        }

        // GET: Result/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Result/Create
        [HttpPost]
        public ActionResult Create(Result result)
        {
            try
            {
                _resultDAO.SaveResult(result);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// UpdateResult(Result result): This method would be responsible for updating an existing result in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        // GET: Result/Edit/5
        public ActionResult Edit(int id)
        {
            Result result = _resultDAO.GetResult(id);
            return View(result);
        }

        // POST: Result/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Result result)
        {
            try
            {
                _resultDAO.Update(result);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Result/Delete/5
        public ActionResult Delete(int id)
        {
            Result result = _resultDAO.GetResult(id);
            return View(result);
        }

        // POST: Result/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Result result)
        {
            try
            {
                _resultDAO.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Result/Pass/5
        public ActionResult Pass(int id)
        {
            Result result = _resultDAO.GetResult(id);
            if (result.Score >= 6)
            {
                return View(result);
            }
            else
            {
                return RedirectToAction("Fail", new { id = id });
            }
        }

        // GET: Result/Fail/5
        public ActionResult Fail(int id)
        {
            Result result = _resultDAO.GetResult(id);
            return View(result);
        }
        /// <summary>
        /// TakeAnotherQuiz(): This method would redirect the user back to the home page where they can take another quiz.
        /// </summary>
        /// <returns></returns>
        // GET: Result/TakeAnotherQuiz
        public ActionResult TakeAnotherQuiz()
        {
            return RedirectToAction("Index", "Home");
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}


        [HttpGet]
        public ActionResult ShowQuizResult(int resultID)
        {
            // Use the ResultDAO to get the result by ID
            Result result = _resultDAO.GetResult(resultID);
            // Use the QuizDAO to get the quiz name
            string quizName = _quizDAO.GetQuizName(result.QuizID);

            // Use the UserDAO to get the user's full name
            string userFullName = _usersDAO.GetFullName(result.UserID);

            // Use the QuizQuestionDAO to get the result of each multi-choice question
            List<QuizQuestion> quizQuestions = _quizQuestionDAO.GetQuizQuestionsByResult(resultID);

            // Check if the user passed the quiz (6 or more questions answered correctly)
            bool passed = false;
            int correctAnswers = 0;
            foreach (var question in quizQuestions)
            {
                if (question.IsCorrect)
                {
                    correctAnswers++;
                }
            }
            if (correctAnswers >= 6)
            {
                passed = true;
            }

            // Create a view model to store the data
            QuizResultViewModel viewModel = new QuizResultViewModel
            {
                Result = result,
                QuizName = quizName,
                QuizQuestions = quizQuestions,
                Passed = passed
            };

            // Return the view with the view model
            return View(viewModel);

        }

        public bool CheckPassingScore(int resultID)
        {
            // Use the ResultDAO to retrieve the result with the specified ID
            Result result = _resultDAO.GetResult(resultID);

            // Use the QuizDAO to retrieve the quiz associated with the result
            Quiz quiz = _quizDAO.GetByID(result.QuizID);

            // Use the QuizQuestionDAO to retrieve the answers for the quiz
            List<QuizQuestion> answers = _quizQuestionDAO.GetAnswers(result.QuizID, result.UserID);

            // Calculate the number of correct answers
            int correctAnswers = answers.Where(a => a.IsCorrect).Count();

            // Check if the user passed the quiz
            if (correctAnswers >= quiz.PassingScore)
            {
                return true;
            }

            return false;
        }

        [HttpPost]
        public ActionResult SaveResult(Result result)
        {
            _resultDAO.SaveResult(result);
            return RedirectToAction("ShowQuizResult", new { resultID = result.ResultID });
        }

        [HttpGet]
        public ActionResult GetResults(int quizID)
        {
            // Use the ResultDAO to retrieve a list of all results
            var results = _resultDAO.GetResults(quizID);
            // Return the results to the view
            return View(results);

        }

        [HttpGet]
        public ActionResult GetResult(int resultID)
        {
            // Use the ResultDAO to retrieve the specified result
            Result result = _resultDAO.GetResult(resultID);
            // If the result is null, return a 404 Not Found
            if (result == null)
            {
                return HttpNotFound();
            }

            // Otherwise, return the result
            return View(result);

        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException("No Result found.");
        }
        //public ActionResult Result(int id)
        //{
        //    Result result = new Result();
        //    result.Questions = _questionDAO.GetByQuizIDAndUserID(id, User.Identity.GetUserId());

        //    return View(result);
        //}

    }
}
