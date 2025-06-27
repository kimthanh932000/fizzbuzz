namespace Backend.Repositories
{
    public class GamePlayRepo : IGamePlayRepo
    {
        private readonly ApplicationDbContext _context;

        public GamePlayRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(GamePlay gamePlay)
        {
            await _context.GamePlays.AddAsync(gamePlay);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(GamePlay gamePlay)
        {
            _context.GamePlays.Remove(gamePlay);
            await _context.SaveChangesAsync();
        }

        public async Task<GamePlay?> GetByIdAsync(int id)
        {
            return await _context.GamePlays
                .Include(gp => gp.Game)
                .FirstOrDefaultAsync(gp => gp.Id == id);
        }

        public async Task UpdateAsync(GamePlay gamePlay)
        {
            _context.GamePlays.Update(gamePlay);
            await _context.SaveChangesAsync();
        }
    }
}
