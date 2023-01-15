using Microsoft.AspNetCore.Mvc;
using QuizApp.DAO;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class FeedbackController : Controller
    {
        private FeedbackDAO _feedbackDAO;
        public FeedbackController(FeedbackDAO feedbackDAO)
        {
            _feedbackDAO = feedbackDAO;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Feedback> feedbacks = _feedbackDAO.GetAll();
            return View(feedbacks);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return View(feedback);
            }

            _feedbackDAO.Create(feedback);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Feedback feedback = _feedbackDAO.GetByID(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException("No feedback found.");
        }

        [HttpPost]
        public ActionResult Edit(Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return View(feedback);
            }

            _feedbackDAO.Update(feedback);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _feedbackDAO.Delete(id);
            return RedirectToAction("Index");
        }


    }   
}
