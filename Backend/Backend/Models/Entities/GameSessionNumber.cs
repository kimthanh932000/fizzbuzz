namespace Backend.Models.Entities
{
    public class GameSessionNumber : BaseEntity
    {
        public int GameSessionId { get; set; }
        public GameSession GameSession { get; set; }  // Navigation property
        public int Value { get; set; }
    }
}
