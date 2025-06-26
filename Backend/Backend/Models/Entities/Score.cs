namespace Backend.Models.Entities
{
    public class Score : BaseEntity
    {
        public int GameId { get; set; }
        public Game Game { get; set; }  // Navigation property
        public int TotalCorrect { get; set; }
        public int TotalIncorrect { get; set; }
    }
}
