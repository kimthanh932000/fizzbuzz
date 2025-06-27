namespace Backend.Models.DTOs
{
    public class CreateGameDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public int Range { get; set; }
        [Required]
        public int DurationInSeconds { get; set; }
        [Required]
        public List<CreateRuleDto> Rules { get; set; }
    }
}
