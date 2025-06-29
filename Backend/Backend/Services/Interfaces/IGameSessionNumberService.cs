namespace Backend.Services.Interfaces
{
    public interface IGameSessionNumberService
    {
        Task AddNewNumberAsync(GameSessionNumber number);
        //Task<bool> IsNumberUsed(int number, int gameSessionId);
        Task<IEnumerable<int>> GetUsedNumbersBySessionIdAsync(int sessionId);
    }
}
