using Microsoft.AspNetCore.Mvc;
using QuizApp.DAO;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class QuestionController : Controller
    {
        private readonly QuestionDAO _questionDAO;
        public QuestionController(QuestionDAO questionDAO)
        {
            _questionDAO = questionDAO;
        }

        public IActionResult Index()
        {
            //return View(_questionDAO.GetAll());
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Question question)
        {
            _questionDAO.Create(question);
            return RedirectToAction("Index");
        }

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

        public IActionResult Delete(int id)
        {
            _questionDAO.Delete(id);
            return RedirectToAction("Index");
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

    }
}
