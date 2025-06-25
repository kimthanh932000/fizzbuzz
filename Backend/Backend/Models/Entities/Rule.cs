using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Entities
{
    public class Rule : BaseEntity
    {
        public int DivisibleBy { get; set; }    // e.g., 11

        public string Word { get; set; }    // e.g., "Foo"

        public int GameId { get; set; }

        public Game Game { get; set; }  // Navigation property
    }
}
