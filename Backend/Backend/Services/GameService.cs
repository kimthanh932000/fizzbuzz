namespace Backend.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepo _gameRepo;

        public GameService(IGameRepo gameRepo)
        {
            _gameRepo = gameRepo;
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
