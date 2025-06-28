namespace Backend.Repositories.Interfaces
{
    public interface IGameSessionNumberRepo : IRepoBase<GameSessionNumber>
    {
        Task<IEnumerable<GameSessionNumber>> GetByGameSessionIdAsync(int gameSessionId);
    }
}
