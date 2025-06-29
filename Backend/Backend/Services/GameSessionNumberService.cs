namespace Backend.Services
{
    public class GameSessionNumberService : IGameSessionNumberService
    {
        private readonly IGameSessionNumberRepo _gameSessionNumberRepo;

        public GameSessionNumberService(IGameSessionNumberRepo gameSessionNumberRepo)
        {
            _gameSessionNumberRepo = gameSessionNumberRepo;
        }

        public async Task AddNewNumberAsync(GameSessionNumber number)
        {
            await _gameSessionNumberRepo.AddAsync(number);
        }

        public async Task<IEnumerable<int>> GetUsedNumbersBySessionIdAsync(int sessionId)
        {
            var usedNumbers = await _gameSessionNumberRepo.GetByGameSessionIdAsync(sessionId);
            return usedNumbers.Select(n => n.Value);
        }

        //public async Task<bool> IsNumberUsed(int number, int sessionId)
        //{
        //    var usedNumbers = await _gameSessionNumberRepo.GetByGameSessionIdAsync(sessionId);
        //    return usedNumbers.Any(n => n.Value == number);
        //}
    }
}