namespace Backend.Services.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game?> GetByIdAsync(int id);
        Task AddAsync(Game game);
        Task UpdateAsync(Game game);
        Task DeleteAsync(int id);
        Task<bool> IsCorrectAnswerAsync(int gameId, int number, string userInput);
    }
}
