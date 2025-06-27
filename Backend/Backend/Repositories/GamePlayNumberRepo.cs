namespace Backend.Repositories
{
    public class GamePlayNumberRepo : IGamePlayNumberRepo
    {
        private readonly ApplicationDbContext _context;

        public GamePlayNumberRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(GamePlayNumber number)
        {
            await _context.GamePlayNumbers.AddAsync(number);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(GamePlayNumber number)
        {
            _context.GamePlayNumbers.Remove(number);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GamePlayNumber>> GetByGamePlayIdAsync(int gamePlayId)
        {
            return await _context.GamePlayNumbers
                          .Where(r => r.GamePlayId == gamePlayId)
                          .ToListAsync();
        }

        public async Task<GamePlayNumber?> GetByIdAsync(int id)
        {
            return await _context.GamePlayNumbers.FindAsync(id);
        }

        public async Task UpdateAsync(GamePlayNumber number)
        {
            _context.GamePlayNumbers.Update(number);
            await _context.SaveChangesAsync();
        }
    }
}
