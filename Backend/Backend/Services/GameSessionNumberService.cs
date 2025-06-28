using Backend.Helpers;

namespace Backend.Services
{
    public class GameSessionNumberService : IGameSessionNumberService
    {
        private readonly IGameSessionService _gameSessionService;
        private readonly IGameSessionNumberRepo _gameSessionNumberRepo;

        public GameSessionNumberService(IGameSessionService gameSessionService, IGameSessionNumberRepo gameSessionNumberRepo)
        {
            _gameSessionService = gameSessionService;
            _gameSessionNumberRepo = gameSessionNumberRepo;
        }

        public async Task<int> GenerateRandomNumber(int gameSessionId)
        {
            int range = 100;
            int randNum = RandomHelper.Generate(1, range);
            return randNum;
        }

        public async Task<bool> IsNumberAlreadyUsed(int inputNumber, int gameSessionId)
        {
            var usedNumbers = await _gameSessionNumberRepo.GetByGameSessionIdAsync(gameSessionId);
            return usedNumbers.Any(n => n.Value == inputNumber);
        }
    }
}