namespace Backend.Models.DTOs
{
    public class CreateRuleDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Divisible by must be positive number.")]
        public int DivisibleBy { get; set; }

        [Required]
        public string Word { get; set; }
    }
}
