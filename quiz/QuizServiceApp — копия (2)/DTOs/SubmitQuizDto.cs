namespace QuizServiceApp.DTOs
{
    public class SubmitQuizDto
    {
        public int QuizId { get; set; }

        public List<StudentAnswerDto> Answers { get; set; }
    }

    public class StudentAnswerDto
    {
        public int QuestionId { get; set; }

        public int AnswerId { get; set; }
    }
}
