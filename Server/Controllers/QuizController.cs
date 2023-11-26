using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class QuizController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public QuizController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Quiz>> Get()
        {
            var userId = GetUserId();
            return await _context.Quizzes.Where(q => q.UserId == userId).ToListAsync();
        }

        [HttpGet("[action]")]
        public async Task<List<Quiz>> Published()
        {
            return await _context.Quizzes.Where(q => q.IsPublished).ToListAsync();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Quiz? quiz = _context.Quizzes.Where(q => q.Id == id).Include(q => q.Questions).FirstOrDefault();
                if (quiz != null)
                {
                    return Ok(quiz);
                }
                return NotFound();
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public void Post(Quiz quiz)
        {
            quiz.UserId = GetUserId();
            _context.Quizzes.Add(quiz);
            _context.SaveChanges();
        }

        [HttpPut]
        public void Put(Quiz quiz)
        {
            _context.Entry(quiz).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Quiz? quiz = _context.Quizzes.Where(q => q.Id == id).Include(q => q.Questions).FirstOrDefault();
            if (quiz != null)
            {
                if (quiz.Questions != null && quiz.Questions.Count != 0)
                {
                    _context.Questions.RemoveRange(quiz.Questions);

                }
                _context.Quizzes.Remove(quiz);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException();
            }
            return Ok();
        }

        private string? GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
