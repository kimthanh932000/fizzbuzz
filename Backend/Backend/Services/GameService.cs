namespace Backend.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepo _gameRepo;
        private readonly IRuleService _ruleService;

        public GameService(IGameRepo gameRepo, IRuleService ruleService)
        {
            _gameRepo = gameRepo;
            _ruleService = ruleService;
        }

        public async Task AddAsync(Game game)
        {
            await _gameRepo.AddAsync(game);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _gameRepo.GetByIdAsync(id);
            if (entity == null)
            {
                await _gameRepo.DeleteAsync(entity);
            }
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _gameRepo.GetAllAsync();
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _gameRepo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Game game)
        {
            await _gameRepo.UpdateAsync(game);
        }

        public async Task<bool> IsCorrectAnswer(int gameId, int number, string userInput)
        {
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
    }
}
