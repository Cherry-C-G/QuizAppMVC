using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.DAO;
using QuizApp.Models;
using System.Diagnostics;

namespace QuizApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CategoryDAO  _categoryDAO;
        private readonly QuizDAO _quizDAO;

        public HomeController(ILogger<HomeController> logger, CategoryDAO categoryDAO, QuizDAO quizDAO)
        {
            _logger = logger;
            _categoryDAO = categoryDAO;
            _quizDAO = quizDAO;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }
        public ActionResult Index()
        {
            List<Quiz> quizzes = _quizDAO.GetAll();
            return View(quizzes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}