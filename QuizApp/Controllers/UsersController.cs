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
        /// <summary>
        /// Register - for handling user registration
        /// </summary>
        /// <returns></returns>
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
            return View("Login");
        }

        /// <summary>
        /// Login - for handling user login
        /// </summary>
        /// <returns></returns>
        //public ActionResult Login()
        //{
        //    return View();
        //}
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
        /// <summary>
        /// Logout - for handling user logout
        /// </summary>
        /// <returns></returns>
        [Authorize]
        //[AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// UpdatePassword - for handling password updates
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdatePassword(string email, string newPassword)
        {
            try
            {
                _userDAO.UpdatePassword(email, newPassword);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// UpdateRole - for handling updates to a user's role
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateRole(int userID, string role)
        {
            try
            {
                _userDAO.UpdateRole(userID, role);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Search - for handling searches for users based on keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult Search(string keyword)
        {
            UsersDAO usersDAO = _userDAO;
            List<Users> users = usersDAO.Search(keyword);
            return View(users);
        }

        /// <summary>
        /// IsEmailExist - for checking if a given email address already exists in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>

        //public ActionResult IsEmailExist(string email)
        //{
        //    UsersDAO usersDAO = _userDAO;
        //    if (usersDAO.IsEmailExist(email))
        //    {
        //        return Json(data: true, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(data: false, JsonRequestBehavior.AllowGet);
        //    }
        //}

        /// <summary>
        /// IsUserValid - for checking if a given email and password combination is valid.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IsUserValid(string email, string password)
        {
            UsersDAO usersDAO = _userDAO;
            bool isValid = usersDAO.IsUserValid(email, password);
            if (isValid)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

    }
}
