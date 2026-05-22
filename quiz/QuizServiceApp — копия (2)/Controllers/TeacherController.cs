using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizServiceApp.Data;
using QuizServiceApp.DTOs;
using QuizServiceApp.Models;
using System.Security.Claims;


namespace QuizServiceApp.Controllers
{
    [ApiController]
    [Route("api/teacher")]

    [Authorize(Roles = "Преподаватель")]
    public class TeacherController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create-quiz")]
        public async Task<IActionResult> CreateQuiz(CreateQuizDto dto)
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var quiz = new Quiz
            {
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                CreatedAt = DateTime.UtcNow,

                TeacherId = int.Parse(userId)
            };

            _context.Quizzes.Add(quiz);

            await _context.SaveChangesAsync();

            return Ok("Квиз создан");
        }

        [HttpPost("add-question")]
        public async Task<IActionResult> AddQuestion(CreateQuestionDto dto)
        {
            var quiz = await _context.Quizzes.FindAsync(dto.QuizId);

            if (quiz == null)
                return BadRequest("Квиз не найден");

            if (dto.Answers == null || dto.Answers.Count == 0)
                return BadRequest("Ответы отсутствуют");

            var question = new Question
            {
                Text = dto.Text,
                QuizId = dto.QuizId
            };

            _context.Questions.Add(question);

            await _context.SaveChangesAsync();

            for (int i = 0; i < dto.Answers.Count; i++)
            {
                var answer = new Answer
                {
                    Text = dto.Answers[i],

                    QuestionId = question.Id,

                    IsCorrect = i == dto.CorrectAnswerIndex
                };

                _context.Answers.Add(answer);
            }

            await _context.SaveChangesAsync();

            return Ok("Вопрос добавлен");
        }

        [HttpGet("results/{quizId}")]
        public async Task<IActionResult>
GetResults(int quizId)
        {
            var results =
            await _context.Attempts
            .Where(x => x.QuizId == quizId)

            .Include(x => x.Student)

            .OrderByDescending(
            x => x.CompletedAt)

            .Select(x => new
            {
                Student =
                x.Student.Name,

                Score =
                x.Score,

                Date =
                x.CompletedAt
            })

            .ToListAsync();

            return Ok(results);
        }

    }

}