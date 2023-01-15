using Microsoft.AspNetCore.Mvc;
using QuizApp.DAO;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class AnswerController : Controller
    {

        private readonly AnswerDAO _answerDAO;

        public AnswerController(AnswerDAO answerDAO)
        {
            _answerDAO = answerDAO;
        }

        /// <summary>
        /// GetAnswers(int questionID) to retrieve a list of answers for a specific question
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        // GET: Answer/GetByQuestionID
        public JsonResult GetAnswers(int questionID)
        {
            var answers = _answerDAO.GetByQuestionID(questionID);
            return Json(answers);
        }


        // POST: Answer/SubmitAnswer
        [HttpPost]
        public ActionResult SubmitAnswer(Answer answer)
        {
            _answerDAO.SubmitAnswer(answer);
            return Json(answer);
        }

        // GET: Answer/CheckAnswer
        public ActionResult CheckAnswer(int answerID)
        {
            var isCorrect = _answerDAO.CheckAnswer(answerID);
            return Ok(answerID);
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// AddAnswer(Answer answer) to add a new answer to the database
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddAnswer(Answer answer)
        {
            // Use the AnswerDAO to add the answer to the database
            _answerDAO.Insert(answer);

            // Return a success message
            return Json(new { success = true, message = "Answer added successfully!" });
        }

        /// <summary>
        /// UpdateAnswer(Answer answer) to update an existing answer in the database
        /// </summary>
        /// <param name="answer"></param>
        [HttpPut]
        public void UpdateAnswer(Answer answer)
        {
            _answerDAO.Update(answer);
        }


        /// <summary>
        /// MarkAnswer(int answerID) to mark an answer as correct or incorrect
        /// </summary>
        /// <param name="answerID"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult MarkAnswer(int answerID)
        {
            _answerDAO.MarkAnswer(answerID);
            ViewBag.Message = "Answer Marked";
            return View("Success");
        }
        /// <summary>
        /// GetCorrectAnswer(int questionID) to retrieve the correct answer for a specific question
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCorrectAnswer(int questionID)
        {
            var correctAnswer = _answerDAO.GetCorrectAnswer(questionID);
            return View(correctAnswer);
        }
        /// <summary>
        /// DeleteAnswer(int answerID) to delete an answer from the database
        /// </summary>
        /// <param name="answerID"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult DeleteAnswer(int answerID)
        {
            _answerDAO.Delete(answerID);
            return new EmptyResult();
        }
        /// <summary>
        /// GetAnswer(int answerID) to retrieve a specific answer by its ID
        /// </summary>
        /// <param name="answerID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAnswer(int answerID)
        {
            var answer = _answerDAO.GetAnswer(answerID);
            return View(answer);
        }
    }
}
