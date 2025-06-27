namespace Backend.Repositories
{
    public class RuleRepo : IRuleRepo
    {
        private readonly ApplicationDbContext _context;

        public RuleRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Rule rule)
        {
            await _context.Rules.AddAsync(rule);
            await _context.SaveChangesAsync();
        }

        public async Task AddRulesAsync(IEnumerable<Rule> rules)
        {
            await _context.Rules.AddRangeAsync(rules);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Rule rule)
        {
            _context.Rules.Remove(rule);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rule>> GetByGameIdAsync(int gameId)
        {
            return await _context.Rules
                .Where(r => r.GameId == gameId)
                .ToListAsync();
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
