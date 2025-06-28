namespace Backend.Repositories
{
    public class GameSessionNumberRepo : IGameSessionNumberRepo
    {
        private readonly ApplicationDbContext _context;

        public GameSessionNumberRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(GameSessionNumber number)
        {
            await _context.GameSessionNumbers.AddAsync(number);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(GameSessionNumber number)
        {
            _context.GameSessionNumbers.Remove(number);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GameSessionNumber>> GetByGameSessionIdAsync(int gameSessionId)
        {
            return await _context.GameSessionNumbers
                          .Where(r => r.GameSessionId == gameSessionId)
                          .ToListAsync();
        }

        public async Task<GameSessionNumber?> GetByIdAsync(int id)
        {
            return await _context.GameSessionNumbers.FindAsync(id);
        }

        public async Task UpdateAsync(GameSessionNumber number)
        {
            _context.GameSessionNumbers.Update(number);
            await _context.SaveChangesAsync();
        }
    }
}
