namespace Backend.Models.DTOs
{
    public static class GameMapper
    {
        public static CreateGameDto ToCreateGameDto(this Game game)
        {
            return new CreateGameDto()
            {
                Name = game.Name,
                AuthorName = game.AuthorName,
                Range = game.Range,
                DurationInSeconds = game.DurationInSeconds,
                Rules = new List<CreateRuleDto>()
            };
        }
    }
}
