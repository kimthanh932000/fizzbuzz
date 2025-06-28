namespace Backend.Services.Interfaces
{
    public interface IGameSessionService
    {
        Task<GameSession?> GetByIdAsync(int id);
        Task StartGameSessionAsync();
        Task<bool> IsCorrectAnswerAsync(int gameId, int number, string userInput);
    }
}
