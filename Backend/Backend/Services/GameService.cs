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
            var existedGameName = await _gameRepo.IsGameNameExistedAsync(game.Name);
            if (existedGameName)
            {
                throw new InvalidOperationException("Game name already exists.");
            }
            await _gameRepo.AddAsync(game);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _gameRepo.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Game was not found.");
            }
            await _gameRepo.DeleteAsync(entity);
        }

        public async Task<IEnumerable<Game?>> GetAllAsync()
        {
            return await _gameRepo.GetAllAsync();
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            var entity = await _gameRepo.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Game was not found.");
            }
            return await _gameRepo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Game game)
        {
            var entity = await _gameRepo.GetByIdAsync(game.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Game was not found.");
            }
            await _gameRepo.UpdateAsync(game);
        }

        public async Task<bool> IsCorrectAnswerAsync(int gameId, int number, string userInput)
        {
            var entity = await _gameRepo.GetByIdAsync(gameId);
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
    }
}
