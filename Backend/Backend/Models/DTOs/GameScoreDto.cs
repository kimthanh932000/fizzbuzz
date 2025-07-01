namespace Backend.Models.DTOs
{
    public class GameScoreDto
    {
        public int SessionId { get; set; }
        public int TotalCorrect { get; set; }
        public int TotalIncorrect { get; set; }
    }
}
