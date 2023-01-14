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

        public QuizController(QuizDAO quizDAO, QuestionDAO questionDAO, AnswerDAO answerDAO)
        {
            _quizDAO = quizDAO;
            _questionDAO = questionDAO;
            _answerDAO = answerDAO;
        }
        public ActionResult Index()
        {
            List<Quiz> quizzes = _quizDAO.GetAll();
            return View(quizzes);
        }

        public ActionResult Create()
        {
            return View();
        }

        //public ActionResult TakeQuiz(int quizId)
        //{
        //    var quiz = _quizDAO.GetQuizById(quizId);
        //    var questions = _questionDAO.GetQuestionsByQuizId(quizId);
        //    var answers = _answerDAO.GetAnswersByQuizId(quizId);

        //    var model = new QuizViewModel
        //    {
        //        Quiz = quiz,
        //        Questions = questions,
        //        Answers = answers
        //    };

        //    return View(model);
        //}

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
