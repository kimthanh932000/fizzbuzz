namespace Backend.Models.Entities
{
    public class GamePlayNumber : BaseEntity
    {
        public int GamePlayId { get; set; }
        public GamePlay GamePlay { get; set; }  // Navigation property
        public int Value { get; set; }
    }
}
