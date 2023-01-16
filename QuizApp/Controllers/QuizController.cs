using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizApp.DAO;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class QuizController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}


        private readonly QuizDAO _quizDAO;
        private readonly QuestionDAO _questionDAO;
        private readonly AnswerDAO _answerDAO;
        private readonly ResultDAO _resultDAO;
        private readonly QuizQuestionDAO _quizQuestionDAO;
       // private readonly IMapper _mapper;
        public QuizController(QuizDAO quizDAO, QuestionDAO questionDAO, AnswerDAO answerDAO, ResultDAO resultDAO, QuizQuestionDAO quizQuestionDAO)
        {
            _quizDAO = quizDAO;
            _questionDAO = questionDAO;
            _answerDAO = answerDAO;
            _resultDAO = resultDAO;
            _quizQuestionDAO = quizQuestionDAO;
           // _mapper = mapper;
        }
        public ActionResult Index()
        {
        
            
            List<Quiz> quiz = _quizDAO.GetAll();
           
            return View(quiz);
        }

        public ActionResult Create()
        {
            return View();
        }

        // GET: Quiz/Start
        public ActionResult Start(int quizID)
        {
            Quiz quiz = _quizDAO.GetByID(quizID);
            if (quiz == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetInt32("quizID", quizID);
            HttpContext.Session.SetInt32("timeRemaining", quiz.TimeLimit);
            return RedirectToAction("QuizScreen");
        }
        public IActionResult QuizScreen(string category)
        {
            // Use the QuizDAO to get the questions for the selected category
            var questions = _quizDAO.GetQuestionsByCategory(category);

            // Create a new QuizViewModel to hold the questions and other information needed for the view
            var model = new QuizQuestionViewModel
            {
                Questions = questions,
            };

            // Return the QuizScreen view, passing the QuizViewModel as the model
            return View("QuizScreen", model);
        }


        //// GET: Quiz/QuizScreen
        //public ActionResult QuizScreen()
        //{
        //    int? quizID = HttpContext.Session.GetInt32("quizID");
        //    if (quizID == null)
        //    {
        //        return RedirectToAction("Home");
        //    }
        //    Quiz quiz = _quizDAO.GetByID((int)quizID);
        //    List<Question> questions = _quizDAO.GetQuestionsByCategory(Categories);
        //    int? timeRemaining = HttpContext.Session.GetInt32("timeRemaining");
        //    if (timeRemaining == null)
        //    {
        //        timeRemaining = quiz.TimeLimit;
        //    }

        //    QuizQuestionViewModel viewModel = new QuizQuestionViewModel
        //    {
        //        Quiz = quiz,
        //        Questions = questions,
        //        CurrentQuestion = 0,
        //        TimeRemaining = (int)timeRemaining
        //    };
        //    return View(viewModel);
        //}

        // POST: Quiz/NextQuestion
        [HttpPost]
        public ActionResult NextQuestion(QuizQuestionViewModel viewModel)
        {
            // update current question index
            viewModel.CurrentQuestion++;
            // check if on last question
            if (viewModel.CurrentQuestion == viewModel.Questions.Count)
            {
                // redirect to submit action
                return RedirectToAction("Submit", viewModel);
            }

            // check if the user has exceeded the time limit
            if (viewModel.TimeRemaining <= 0)
            {
                // show alert and force submit
                TempData["timeExceeded"] = true;
                return RedirectToAction("Submit", viewModel);
            }

            // update question styles
            viewModel.QuestionStyles = _quizDAO.GetQuestionStyles(viewModel.Quiz.QuizID, viewModel.CurrentQuestion);

            // return view
            return View("QuizScreen", viewModel);
        }

        public IActionResult Categories()
        {
            List<Quiz> quizTypes = _quizDAO.GetQuizTypes();
            return View(quizTypes);
        }
        public IActionResult SelectCategory(string quizType)
        {
            List<Quiz> quizzes = _quizDAO.GetQuizzesByType(quizType);
            return View(quizzes);
        }

        public IActionResult TakeQuiz(string category)
        {
            //Quiz quiz = _quizDAO.Create(newQuiz);
            List<Question> questions = _quizDAO.GetQuestionsByCategory(category);

            QuizQuestionViewModel viewModel = new QuizQuestionViewModel()
            {
                //Quiz = quiz,
                Questions = questions
            };

            return View(viewModel);
        }
        public IActionResult QuizTypes()
        {
            QuizDAO quizDAO = _quizDAO;
            List<Quiz> quizTypes = quizDAO.GetQuizTypes();
            return View(quizTypes);
        }
        public IActionResult DisplayCategory(string quizType)
        {
            var quizzes = _quizDAO.GetQuizzesByType(quizType);
            return View(quizzes);
        }

        //public IActionResult SelectCategory(string quizType)
        //{
        //    var questions = _questionDAO.GetQuestionsByType(quizType);
        //    return View(questions);
        //}
        //public IActionResult TakeQuizQuestion(string category)
        //{
        //    var questions = _quizDAO.GetQuestionsByCategory(category);

        //    var viewModel = new QuizQuestionViewModel
        //    {
        //        Questions = (List<Question>)questions
        //    };

        //    return View(viewModel);
        //}
        [HttpPost]
        public IActionResult SubmitQuiz(QuizQuestionViewModel viewModel)
        {
            int score = 0;
            for (int i = 0; i < viewModel.Questions.Count; i++)
            {
                if (viewModel.Questions[i].SelectedAnswer == viewModel.Questions[i].CorrectAnswer)
                {
                    score++;
                }
            }

            return View(score);
        }


        public ActionResult PreviousQuestion(QuizQuestionViewModel viewModel)
        {
            // update current question index
            viewModel.CurrentQuestion--;
            // check if on first question
            if (viewModel.CurrentQuestion == 0)
            {
                // redirect to home page
                return RedirectToAction("Index", "Home");
            }
            // update question styles
            viewModel.QuestionStyles = _quizDAO.GetQuestionStyles(viewModel.Quiz.QuizID, viewModel.CurrentQuestion);

            // return view
            return View("QuizScreen", viewModel);
        }

        //[HttpPost]
        //public ActionResult SubmitQuiz(QuizQuestionViewModel viewModel)
        //{
        //    // check if all questions have been answered
        //    if (!_quizDAO.CheckIfAllQuestionsAnswered(viewModel.Quiz.QuizID, viewModel.UserID))
        //    {
        //        TempData["notAllAnswered"] = true;
        //        return View("QuizScreen", viewModel);
        //    }

        //    // save the score to the database
        //    _resultDAO.SaveScore(viewModel.Quiz.QuizID, viewModel.UserID, _resultDAO.CalculateScore(viewModel.Quiz.QuizID, viewModel.UserID, viewModel.AnswerText));

        //    // redirect to results
        //    return RedirectToAction("Results", new { quizID = viewModel.Quiz.QuizID });
        //}

        public ActionResult Results(int quizID)
        {
            // get the results for the quiz
            var results = _resultDAO.GetResults(quizID);

            // return the results view
            return View(results);
        }



        [HttpPost]
        public ActionResult Create(Quiz quiz)
        {
            _quizDAO.Create(quiz);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Quiz quiz = _quizDAO.GetById(id);
            return View(quiz);
        }

        [HttpPost]
        public ActionResult Edit(Quiz quiz)
        {
            _quizDAO.Update(quiz);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _quizDAO.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
