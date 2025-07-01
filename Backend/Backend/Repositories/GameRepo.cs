namespace Backend.Repositories
{
    public class GameRepo : IGameRepo
    {
        private readonly ApplicationDbContext _context;

        public GameRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Game> AddAsync(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task DeleteAsync(Game game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game?>> GetAllAsync()
        {
            return await _context.Games.Include(g => g.Rules).ToListAsync();
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _context.Games.Include(g => g.Rules).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<bool> IsGameNameExistedAsync(string name)
        {
            return await _context.Games
                .AnyAsync(g => g.Name.ToLower() == name.ToLower());
        }

        public async Task UpdateAsync(Game game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }
    }
}
