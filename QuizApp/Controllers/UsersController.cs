using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.DAO;
using QuizApp.Models;
using RestSharp;
using System.Security.Claims;

namespace QuizApp.Controllers
{
    public class UsersController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly UsersDAO _userDAO;
        private readonly QuizDAO _quizDAO;

        public UsersController(UsersDAO usersDAO, QuizDAO quizDAO)
        {
            _userDAO = usersDAO;
            _quizDAO = quizDAO;
        }
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Registration(Users user)
        //{
        //    _userDAO.Create(user);
        //    return RedirectToAction("Login");
        //}

        [HttpPost]
        public ActionResult Registration(Users user)
        {
            if (user.Role != "User" && user.Role != "Admin")
            {
                ModelState.AddModelError("Role", "Invalid Role");
            }
            if (ModelState.IsValid)
            {
                _userDAO.Create(user);
                return RedirectToAction("Index");
            }
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }
        //[AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = _userDAO.GetByEmail(email);
            if (user != null && user.Password == password)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
            new Claim(ClaimTypes.Role, user.Role)
        };
                var identity = new ClaimsIdentity(claims, "ApplicationCookie");
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal, new AuthenticationProperties
                {
                    IsPersistent = true
                });
                return RedirectToAction("Home");
            }
            ViewBag.Error = "Invalid Email or Password";
            return View();
        }



        public ActionResult Home()
        {
            var quizzes = _quizDAO.GetAll();
            return View(quizzes);
        }
        [Authorize]
        //[AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}
