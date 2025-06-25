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
    }
}
