namespace Backend.Repositories.Interfaces
{
    public interface IGamePlayNumberRepo : IRepoBase<GamePlayNumber>
    {
        Task<IEnumerable<GamePlayNumber>> GetByGamePlayIdAsync(int gamePlayId);
    }
}
