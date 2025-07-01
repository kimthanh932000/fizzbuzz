namespace Backend.Services.Interfaces
{
    public interface IGameSessionService
    {
        Task<RequestSessionDto> GetByIdAsync(int sessionId);
        Task<RequestSessionDto> StartSessionAsync(int gameId);
        Task ValidateAnswerAsync(int sessionId, int number, string answer);
        Task<int> GetRandomNumber(int sessionId);
        Task<GameScoreDto> GetScoreBySessionIdAsync(int sessionId);
    }
}
