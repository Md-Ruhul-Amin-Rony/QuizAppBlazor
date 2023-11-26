using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAppBlazor.Server.Data;
using QuizAppBlazor.Shared;
using System.Security.Claims;

namespace QuizApplication.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AnswerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public void Post(Answer answer)
        {

            var question = _context.Questions.Where(q => q.Id == answer.QuestionId).FirstOrDefault();

            answer.IsCorrectAnswer = String.Equals(question.CorrectAnswer, answer.Text, StringComparison.OrdinalIgnoreCase);


            answer.UserId = GetUserId();

            var savedAnswer = _context.Answers.Where(q => (q.QuestionId == answer.QuestionId && q.UserId == answer.UserId)).FirstOrDefault();

            if (savedAnswer == null)
            {
                _context.Answers.Add(answer);
            }
            else
            {
                savedAnswer.Text = answer.Text;
                savedAnswer.IsCorrectAnswer = answer.IsCorrectAnswer;
                _context.Entry(savedAnswer).State = EntityState.Modified;
            }

            _context.SaveChanges();


        }
        
        private string? GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}