namespace Backend.Repositories
{
    public class RuleRepo : IRuleRepo
    {
        private readonly ApplicationDbContext _context;

        public async Task AddAsync(Rule rule)
        {
            await _context.AddAsync(rule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Rule rule)
        {
            _context.Rules.Remove(rule);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rule>> GetAllAsync()
        {
            return await _context.Rules.ToListAsync();
        }

        public async Task<Rule?> GetByIdAsync(int id)
        {
            return await _context.Rules.FindAsync(id);
        }

        public async Task UpdateAsync(Rule rule)
        {
            _context.Rules.Update(rule);
            await _context.SaveChangesAsync();
        }
    }
}
