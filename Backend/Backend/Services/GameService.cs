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

        public async Task<RequestGameDto> AddAsync(CreateGameDto createGameDto)
        {
            var existedGameName = await _gameRepo.IsGameNameExistedAsync(createGameDto.Name);
            if (existedGameName)
            {
                throw new FieldValidateException("Name", "Game name already exists.");
            }

            var game = new Game
            {
                Name = createGameDto.Name,
                AuthorName = createGameDto.AuthorName,
                Range = createGameDto.Range,
                DurationInSeconds = createGameDto.DurationInSeconds,
                Rules = new List<Rule>() // Initially empty
            };

            // Save to get GameId
            var result = await _gameRepo.AddAsync(game);

            // Assign Rules with GameId
            game.Rules = createGameDto.Rules.Select(r => new Rule
            {
                DivisibleBy = r.DivisibleBy,
                Word = r.Word,
                GameId = game.Id
            }).ToList();

            var rulesEntity = await _ruleService.AddRulesAsync(game.Rules);

            // Populate Rules into result before returning
            result.Rules = rulesEntity.ToList();

            var gameDto = new RequestGameDto
            {
                Id = result.Id,
                Name = result.Name,
                AuthorName = result.AuthorName, 
                Range = result.Range,
                DurationInSeconds = result.DurationInSeconds,
                Rules = result.Rules.Select(r => new RequestRuleDto
                {
                    Id = r.Id,
                    DivisibleBy = r.DivisibleBy,
                    Word = r.Word
                }).ToList()
            };

            return gameDto;
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
            return entity;
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
    }
}
