namespace Backend.Models.Entities
{
    public class GameSession : BaseEntity
    {
        public int GameId { get; set; }
        public Game Game { get; set; }  // Navigation property
        public DateTime StartTime { get; set; }
        public int RemainingSeconds { get; set; }
        public int TotalCorrect { get; set; }
        public int TotalIncorrect { get; set; }
        public bool IsExpired { get; set; }
        public List<GameSessionNumber> PlayNumbers { get; set; }
    }
}
