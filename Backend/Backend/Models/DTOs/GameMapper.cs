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

        public static CreateRuleDto ToCreateRuleDto(this Rule rule)
        {
            return new CreateRuleDto()
            {
                DivisibleBy = rule.DivisibleBy,
                Word = rule.Word
            };
        }

        public static RequestGameDto ToRequestGameDto(this Game game)
        {
            return new RequestGameDto()
            {
                Id = game.Id,
                Name = game.Name,
                AuthorName = game.AuthorName,
                Range = game.Range,
                DurationInSeconds = game.DurationInSeconds,
            };
        }

        public static RequestSessionDto ToRequestSessionDto(this GameSession session)
        {
            return new RequestSessionDto()
            {
                Id = session.Id,
                GameId = session.GameId,
                RemainingSeconds = session.RemainingSeconds
            };
        }
    }
}
