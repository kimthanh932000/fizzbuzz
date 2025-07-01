namespace Backend.Models.DTOs
{
    public class RequestSessionDto
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int RemainingSeconds { get; set; }
        public bool IsExpired { get; set; }
        public int CurrentNumber { get; set; }
    }
}
