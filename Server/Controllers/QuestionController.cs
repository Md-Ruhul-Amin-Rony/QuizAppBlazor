using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAppBlazor.Server.Data;
using QuizAppBlazor.Shared;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace QuizApplication.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostEnvironment _env;

        public QuestionController(ApplicationDbContext context, IHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Question? question = _context.Questions.Find(id);
                if (question != null)
                {
                    return Ok(question);
                }
                return NotFound();
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public void Post(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();
        }

        [HttpPut]
        public void Put(Question question)
        {
            if(question.Content!=null && question.FileName != null)
            {

                var clientroot = _env.ContentRootPath.Replace("\\Server", "\\Client\\");

                var path = Path.Combine(clientroot, "wwwroot", "uploads", question.FileName);

                question.ImageOrVideoUrl = path;
            }

            _context.Entry(question).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Question? question = _context.Questions.Find(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException();
            }
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UploadFile([FromForm] IEnumerable<IFormFile> files)
        {
            try
            {
                foreach (var file in files)
                {
                    var clientroot = _env.ContentRootPath.Replace("\\Server", "\\Client\\");

                    var path = Path.Combine(clientroot, "wwwroot", "uploads", file.FileName);

                    using (var fileStream = new FileStream(path,  FileMode.Create))
                    {
                        await file.OpenReadStream().CopyToAsync(fileStream);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }


    }
}
