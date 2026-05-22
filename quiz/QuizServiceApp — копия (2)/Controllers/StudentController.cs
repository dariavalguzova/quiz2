using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizServiceApp.Data;
using QuizServiceApp.DTOs;
using QuizServiceApp.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace QuizServiceApp.Controllers
{
    [ApiController]
    [Route("api/student")]

    [Authorize(Roles = "Студент")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(
            AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("submit")]
        public async Task<IActionResult>
        SubmitQuiz(SubmitQuizDto dto)
        {
            var userId =
            User.FindFirstValue(
            ClaimTypes.NameIdentifier);

            int score = 0;

            foreach (var studentAnswer
                in dto.Answers)
            {
                var answer =
                await _context.Answers
                .FirstOrDefaultAsync(x =>
                x.Id ==
                studentAnswer.AnswerId);

                if (answer != null &&
                    answer.IsCorrect)
                {
                    score++;
                }
            }

            var attempt =
            new Attempt
            {
                StudentId =
                int.Parse(userId!),

                QuizId =
                dto.QuizId,

                Score =
                score,

                CompletedAt =
                DateTime.UtcNow
            };

            _context.Attempts
            .Add(attempt);

            await _context
            .SaveChangesAsync();

            return Ok(new
            {
                Score = score
            });
        }
    }
}
