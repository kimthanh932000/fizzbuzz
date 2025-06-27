using Backend.Helpers;

namespace Backend.Services
{
    public class GamePlayNumberService : IGamePlayNumberService
    {
        private readonly IGamePlayService _gamePlayService;
        private readonly IGamePlayNumberRepo _gamePlayNumberRepo;

        public GamePlayNumberService(IGamePlayService gamePlayService, IGamePlayNumberRepo gamePlayNumberRepo)
        {
            _gamePlayService = gamePlayService;
            _gamePlayNumberRepo = gamePlayNumberRepo;
        }

        public async Task<int> GenerateRandomNumber(int gamePlayId)
        {
            int range = 100;
            int randNum = RandomHelper.Generate(1, range);
            return randNum;
        }

        public async Task<bool> IsNumberAlreadyUsed(int inputNumber, int gamePlayId)
        {
            var usedNumbers = await _gamePlayNumberRepo.GetByGamePlayIdAsync(gamePlayId);
            return usedNumbers.Any(n => n.Value == inputNumber);
        }
    }
}