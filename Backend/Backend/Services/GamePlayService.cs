namespace Backend.Services
{
    public class GamePlayService : IGamePlayService
    {
        private readonly IGameService _gameService;
        private readonly IRuleService _ruleService;
        private readonly IGamePlayRepo _gamePlayRepo;

        public GamePlayService(IGameService gameService, IRuleService ruleService, IGamePlayRepo gamePlayRepo)
        {
            _gameService = gameService;
            _ruleService = ruleService;
            _gamePlayRepo = gamePlayRepo;
        }

        public async Task<GamePlay?> GetByIdAsync(int id)
        {
            var entity = await _gamePlayRepo.GetByIdAsync(id);
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
