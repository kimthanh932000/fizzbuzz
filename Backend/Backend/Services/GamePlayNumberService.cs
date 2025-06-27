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

        public Task GenerateRandomNumber(int gamePlayId)
        {
            throw new NotImplementedException();
        }

        //public Task<GamePlay> GetGamePlayById()
    }
}
