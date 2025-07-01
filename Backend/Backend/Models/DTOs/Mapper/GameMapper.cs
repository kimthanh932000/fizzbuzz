namespace Backend.Models.DTOs.Mapper
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

        public static RequestRuleDto ToRequestRuleDto(this Rule rule)
        {
            return new RequestRuleDto()
            {
                Id = rule.Id,
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
                Rules = game.Rules.Select(r => r.ToRequestRuleDto()).ToList()
            };
        }

        public static RequestSessionDto ToRequestSessionDto(this GameSession session)
        {
            return new RequestSessionDto()
            {
                Id = session.Id,
                GameId = session.GameId,
                RemainingSeconds = session.RemainingSeconds,
                IsExpired = session.IsExpired
            };
        }

        public static GameScoreDto ToGameScoreDto(this GameSession session)
        {
            return new GameScoreDto()
            {
                SessionId = session.Id,
                TotalCorrect = session.TotalCorrect,
                TotalIncorrect = session.TotalIncorrect
            };
        }
    }
}
