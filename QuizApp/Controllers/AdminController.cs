using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizApp.DAO;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly UsersDAO _usersDAO;
        private readonly QuizDAO _quizDAO;
        private readonly FeedbackDAO _feedbackDAO;
        private readonly QuestionDAO _questionDAO;
        private readonly AnswerDAO _answerDAO;
        private readonly IMapper _mapper;

        public AdminController(UsersDAO usersDAO, QuizDAO quizDAO, FeedbackDAO feedbackDAO, QuestionDAO questionDAO, AnswerDAO answerDAO, IMapper mapper)
        {
            _usersDAO = usersDAO;
            _quizDAO = quizDAO;
            _feedbackDAO = feedbackDAO;
            _questionDAO = questionDAO;
            _answerDAO = answerDAO;
            _mapper = mapper;
        }

        // GET: Admin/Index
        public ActionResult Index()
        {
            // Retrieve list of users
            List<Users> users = _usersDAO.GetAll();
            var AdminViewAnswersViewModel = _mapper.Map<AdminViewAnswersViewModel>(users);
            return View(users);
        }

        //// GET: Admin/ViewAnswers/5
        //public ActionResult ViewAnswers(int id)
        //{
        //    // Retrieve list of short answer questions and answers for the user
        //    List<QuizQuestion> quizQuestions = _quizDAO.GetShortAnswerQuestionsByUser(id);

        //    return View(quizQuestions);
        //}

        // GET: Admin/ViewFeedback
        public ActionResult ViewFeedback()
        {
            // Retrieve list of feedback
            List<Feedback> feedback = _feedbackDAO.GetAll();

            return View(feedback);
        }

        // GET: Admin/DeleteUser/5
        public ActionResult DeleteUser(int id)
        {
            _usersDAO.Delete(id);

            return RedirectToAction("Index");
        }

        // GET: Admin/ViewAnswers/5
        public ActionResult ViewAnswers(int id)
        {
            Users users = _usersDAO.Get(id);
            List<Quiz> quizzes = _quizDAO.GetAllByUserID(id);
            List<Question> questions = new List<Question>();
            List<Answer> answers = new List<Answer>();

            // Retrieve answers for each quiz and question
            foreach (var quiz in quizzes)
            {
                questions.AddRange(_questionDAO.GetByQuizID(quiz.QuizID));
                foreach (var question in questions)
                {
                    answers.AddRange(_answerDAO.GetByQuestionID(question.QuestionID));
                }
            }

            // Set up view model
            AdminViewAnswersViewModel viewModel = new AdminViewAnswersViewModel
            {
                Users = users,
                Quizzes = quizzes,
                Questions = questions,
                Answers = answers
            };

            // Return view
            return View(viewModel);
        }


    }


    //public IActionResult Index()
    //{
    //    return View();
    //}
}

