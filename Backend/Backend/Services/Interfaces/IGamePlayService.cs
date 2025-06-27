namespace Backend.Services.Interfaces
{
    public interface IGamePlayService
    {
        Task<GamePlay?> GetByIdAsync(int id);
        Task StartGameSessionAsync();
        Task<bool> IsCorrectAnswerAsync(int gameId, int number, string userInput);
    }
}
