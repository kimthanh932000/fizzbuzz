using Backend.Helpers;

namespace Backend.Services
{
    public class GameSessionService : IGameSessionService
    {
        private readonly IGameSessionRepo _gameSessionRepo;
        private readonly IGameService _gameService;
        private readonly IRuleService _ruleService;
        private readonly IGameSessionNumberService _gameSessionNumberService;

        public GameSessionService(
            IGameSessionRepo gameSessionRepo,
            IGameService gameService,
            IRuleService ruleService,
            IGameSessionNumberService gameSessionNumberService)
        {
            _gameSessionRepo = gameSessionRepo;
            _gameService = gameService;
            _ruleService = ruleService;
            _gameSessionNumberService = gameSessionNumberService;
        }

        private async Task ExpireSessionIfNecessaryAsync(GameSession session, bool throwIfExpired = false)
        {
            if (!session.IsExpired)
            {
                session.IsExpired = IsSessionExpired(session.StartTime, session.Game.DurationInSeconds);
                await _gameSessionRepo.UpdateAsync(session);
            }

            if (throwIfExpired && session.IsExpired)
            {
                throw new SessionExpiredException();
            }
        }

        public bool IsSessionExpired(DateTime startTime, int remainingTimeInSeconds)
        {
            // Calculate remaining seconds
            return TimeHelper.GetReminingTimeInSeconds(startTime, remainingTimeInSeconds) == 0;
        }

        private async Task<GameSession> GetSessionOrThrowAsync(int sessionId)
        {
            return await _gameSessionRepo.GetByIdAsync(sessionId)
                   ?? throw new KeyNotFoundException("Session not found.");
        }

        public async Task<GameSession?> GetByIdAsync(int id)
        {
            var session = await GetSessionOrThrowAsync(id);
            await ExpireSessionIfNecessaryAsync(session, throwIfExpired: true);
            return session;
        }

        public async Task ValidateAnswerAsync(int sessionId, int number, string answer)
        {
            var session = await GetSessionOrThrowAsync(sessionId);

            await ExpireSessionIfNecessaryAsync(session, throwIfExpired: true);

            string correctAnswer = "";

            var gameRules = await _ruleService.GetByGameIdAsync(session.Game.Id);

            // Get word substitution based on the game's rules
            foreach (var rule in gameRules)
            {
                if (number % rule.DivisibleBy == 0)
                {
                    correctAnswer += rule.Word;
                }
            }

            // Randomly generated number is not divisible by any of the numbers specified in the game's rules
            if (String.IsNullOrEmpty(correctAnswer))
            {
                correctAnswer = answer;
            }

            // Compare correct answer with input answer
            if (correctAnswer == answer)
            {
                session.TotalCorrect += 1;
            }
            else
            {
                session.TotalIncorrect += 1;
            }
            await _gameSessionRepo.UpdateAsync(session);
        }

        public async Task<GameSession> StartSessionAsync(int gameId)
        {
            var game = await _gameService.GetByIdAsync(gameId);

            if (game == null)
            {
                throw new KeyNotFoundException("Game was not found.");
            }

            var session = await _gameSessionRepo.AddAsync(new GameSession()
            {
                GameId = gameId,
                StartTime = DateTime.UtcNow,
                RemainingSeconds = game.DurationInSeconds,
                IsExpired = false,
                TotalCorrect = 0,
                TotalIncorrect = 0
            });

            return session;
        }

        public async Task<int> GetRandomNumber(int sessionId)
        {
            var session = await GetSessionOrThrowAsync(sessionId);

            await ExpireSessionIfNecessaryAsync(session, throwIfExpired: true);

            var usedNumbers = await _gameSessionNumberService.GetUsedNumbersBySessionIdAsync(sessionId);
            if (usedNumbers.ToList().Count >= session.Game.Range)
            {
                throw new InvalidOperationException("All possible numbers have been used for this session.");
            }

            int randNumber;
            do
            {
                randNumber = RandomHelper.Generate(1, session.Game.Range);
            }
            while (usedNumbers.Contains(randNumber));

            // Save the new number to DB
            var sessionNumber = new GameSessionNumber
            {
                GameSessionId = sessionId,
                Value = randNumber
            };

            await _gameSessionNumberService.AddNewNumberAsync(sessionNumber);

            return sessionNumber.Value;
        }
    }
}
