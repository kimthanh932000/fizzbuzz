namespace Backend.Models.DTOs
{
    public class RequestSessionDto
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int RemainingSeconds { get; set; }
    }
}
