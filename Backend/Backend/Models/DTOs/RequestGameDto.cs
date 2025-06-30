namespace Backend.Models.DTOs
{
    public class RequestGameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public int Range { get; set; }
        public int DurationInSeconds { get; set; }
        public List<RequestRuleDto> Rules { get; set; }
    }
}
