namespace Backend.Models.DTOs
{
    public class CreateRuleDto
    {
        [Required]
        public int DivisibleBy { get; set; }
        [Required]
        public string Word { get; set; }
    }
}
