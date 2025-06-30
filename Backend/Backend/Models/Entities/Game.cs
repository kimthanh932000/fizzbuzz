namespace Backend.Models.Entities
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }    // Game name

        public string AuthorName { get; set; }  // Author name

        [Range(1, int.MaxValue, ErrorMessage = "Range must be a positive number.")]
        public int Range { get; set; }  // Range of numbers

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be a positive number.")]
        public int DurationInSeconds { get; set; }   // Game duration

        public List<Rule> Rules { get; set; }   // List of game rules
        public List<GameSession> GameSessions { get; set; } // List of game sessions
    }
}
