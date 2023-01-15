using Microsoft.AspNetCore.Mvc;
using QuizApp.DAO;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryDAO _categoryDAO;
        public CategoryController(CategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllCategories()
        {
            List<Category> categories = _categoryDAO.GetAll();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryDAO.Create(category);
                return RedirectToAction("GetAllCategories");
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category category = _categoryDAO.GetByID(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException("No Category found.");
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryDAO.Update(category);
                return RedirectToAction("GetAllCategories");
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _categoryDAO.Delete(id);
            return RedirectToAction("GetAllCategories");
        }
    }

}

