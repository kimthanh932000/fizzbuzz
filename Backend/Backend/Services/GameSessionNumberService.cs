using Backend.Helpers;

namespace Backend.Services
{
    public class GameSessionNumberService : IGameSessionNumberService
    {
        //private readonly IGameSessionService _gameSessionService;
        private readonly IGameSessionNumberRepo _gameSessionNumberRepo;

        public GameSessionNumberService(IGameSessionNumberRepo gameSessionNumberRepo)
        {
            //_gameSessionService = gameSessionService;
            _gameSessionNumberRepo = gameSessionNumberRepo;
        }

        public int GenerateRandomNumber(int range)
        {
            int randNum = RandomHelper.Generate(1, range);
            return randNum;
        }

        public async Task<bool> IsNumberUsed(int number, int sessionId)
        {
            var usedNumbers = await _gameSessionNumberRepo.GetByGameSessionIdAsync(sessionId);
            return usedNumbers.Any(n => n.Value == number);
        }
    }
}