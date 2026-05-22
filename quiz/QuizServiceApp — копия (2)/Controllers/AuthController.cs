using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizServiceApp.Data;
using QuizServiceApp.DTOs;
using QuizServiceApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizServiceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(
            AppDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(x => x.Email == dto.Email))
                return BadRequest("Пользователь уже существует");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),

                Role = string.IsNullOrEmpty(dto.Role)
                    ? "Студент"
                    : dto.Role
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok("Регистрация успешна");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
                return Unauthorized("Пользователь не найден");

            bool validPassword =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash);

            if (!validPassword)
                return Unauthorized("Неверный пароль");

            var claims = new[]
            {
        new Claim(
    ClaimTypes.NameIdentifier,
    user.Id.ToString()),

    new Claim(
    ClaimTypes.Name,
    user.Name),

    new Claim(
    ClaimTypes.Email,
    user.Email),

    new Claim(
    ClaimTypes.Role,
    user.Role)
    };

            var jwtKey = _configuration["JwtSettings:Key"];

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey!)
            );

            var creds =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler()
                .WriteToken(token),

                user.Name,
                user.Email,
                user.Role
            });
        }
    }
    
}
