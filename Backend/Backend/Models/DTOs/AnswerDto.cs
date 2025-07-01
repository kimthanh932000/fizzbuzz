namespace Backend.Models.DTOs
{
    public class AnswerDto
    {

        [Required]
        public int Number { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
