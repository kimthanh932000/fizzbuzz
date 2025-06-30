namespace Backend.Services.Interfaces
{
    public interface IGameSessionService
    {
        Task<GameSession?> GetByIdAsync(int sessionId);
        Task<GameSession> StartSessionAsync(int gameId);
        Task ValidateAnswerAsync(int sessionId, int number, string answer);
        Task<int> GetRandomNumber(int sessionId);
    }
}
