namespace Backend.Models.Entities
{
    public class GamePlay : BaseEntity
    {
        public int GameId { get; set; }
        public Game Game { get; set; }  // Navigation property
        public int RemainingSeconds { get; set; }
        public int TotalCorrect { get; set; }
        public int TotalIncorrect { get; set; }
    }
}
