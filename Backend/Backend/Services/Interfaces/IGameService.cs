namespace Backend.Services.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game?> GetByIdAsync(int id);
        Task<RequestGameDto> AddAsync(CreateGameDto game);
        Task UpdateAsync(Game game);
        Task DeleteAsync(int id);
    }
}
