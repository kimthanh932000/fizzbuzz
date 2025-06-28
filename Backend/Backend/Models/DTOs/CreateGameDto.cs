namespace Backend.Models.DTOs
{
    public class CreateGameDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Range must be a positive number.")]
        public int Range { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be a positive number.")]
        public int DurationInSeconds { get; set; }

        [Required]
        public List<CreateRuleDto> Rules { get; set; }
    }
}
