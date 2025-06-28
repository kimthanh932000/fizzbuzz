namespace Backend.Models.Entities
{
    public class Rule : BaseEntity
    {
        [Range(1, int.MaxValue, ErrorMessage = "Divisible by must be positive number.")]
        public int DivisibleBy { get; set; }    // e.g., 11

        public string Word { get; set; }    // e.g., "Foo"

        public int GameId { get; set; }

        public Game Game { get; set; }  // Navigation property
    }
}
