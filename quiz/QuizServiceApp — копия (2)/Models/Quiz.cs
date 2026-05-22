using System.ComponentModel.DataAnnotations.Schema;


namespace QuizServiceApp.Models
{
    public class Quiz
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public DateTime CreatedAt { get; set; }

        public int TeacherId { get; set; }

        public List<Question>? Questions { get; set; }
    }
}
