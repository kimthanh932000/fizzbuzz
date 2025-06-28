namespace Backend.Services
{
    public class GameSessionService : IGameSessionService
    {
        private readonly IGameService _gameService;
        private readonly IRuleService _ruleService;
        private readonly IGameSessionRepo _gameSessionRepo;

        public GameSessionService(IGameService gameService, IRuleService ruleService, IGameSessionRepo gameSessionRepo)
        {
            _gameService = gameService;
            _ruleService = ruleService;
            _gameSessionRepo = gameSessionRepo;
        }

        public async Task<GameSession?> GetByIdAsync(int id)
        {
            var entity = await _gameSessionRepo.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Game play was not found.");
            }
            return entity;
        }

        public async Task<bool> IsCorrectAnswerAsync(int gameId, int number, string userInput)
        {
            var entity = await _gameService.GetByIdAsync(gameId);
            if (entity == null)
            {
                throw new KeyNotFoundException("Game was not found.");
            }
            string correctAnswer = "";

            var gameRules = await _ruleService.GetByGameIdAsync(gameId);

            foreach (var rule in gameRules)
            {
                if (number % rule.DivisibleBy == 0)
                {
                    correctAnswer += rule.Word;
                }
            }
            return correctAnswer == userInput;
        }

        public Task StartGameSessionAsync()
        {
            throw new NotImplementedException();
        }
    }
}
