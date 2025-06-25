namespace Backend.Models.Entities
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }    // Game name

        public string AuthorName { get; set; }  // Author name

        public List<Rule> Rules { get; set; }   // List of game rules
    }
}
