namespace Backend.Repositories
{
    public class GameSessionRepo : IGameSessionRepo
    {
        private readonly ApplicationDbContext _context;

        public GameSessionRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(GameSession gameSession)
        {
            await _context.GameSessions.AddAsync(gameSession);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(GameSession gameSession)
        {
            _context.GameSessions.Remove(gameSession);
            await _context.SaveChangesAsync();
        }

        public async Task<GameSession?> GetByIdAsync(int id)
        {
            return await _context.GameSessions
                .Include(gp => gp.Game)
                .FirstOrDefaultAsync(gp => gp.Id == id);
        }

        public async Task UpdateAsync(GameSession gameSession)
        {
            _context.GameSessions.Update(gameSession);
            await _context.SaveChangesAsync();
        }
    }
}
