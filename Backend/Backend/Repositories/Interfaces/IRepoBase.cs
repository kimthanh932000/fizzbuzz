namespace Backend.Repositories.Interfaces
{
    public interface IRepoBase<T> where T : BaseEntity
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game?> GetByIdAsync(int id);
        Task AddAsync(Game game);
        Task UpdateAsync(Game game);
        Task DeleteAsync(Game game);
    }
}