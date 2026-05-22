using Microsoft.EntityFrameworkCore;
using QuizServiceApp.Models;

namespace QuizServiceApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Quiz> Quizzes => Set<Quiz>();

        public DbSet<Question> Questions => Set<Question>();

        public DbSet<Answer> Answers => Set<Answer>();

        public DbSet<Attempt> Attempts => Set<Attempt>();
    }
}
