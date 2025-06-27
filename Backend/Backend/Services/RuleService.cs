
namespace Backend.Services
{
    public class RuleService : IRuleService
    {
        private readonly IRuleRepo _ruleRepo;

        public RuleService(IRuleRepo ruleRepo)
        {
            _ruleRepo = ruleRepo;
        }

        public async Task AddAsync(Rule rule)
        {
            await _ruleRepo.AddAsync(rule);
        }

        public async Task AddRulesAsync(List<Rule> rules)
        {
            await _ruleRepo.AddRulesAsync(rules);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _ruleRepo.GetByIdAsync(id);
            if (entity != null)
            {
                await _ruleRepo.DeleteAsync(entity);
            }
        }

        public async Task<IEnumerable<Rule>> GetByGameIdAsync(int gameId)
        {
            return await _ruleRepo.GetByGameIdAsync(gameId);
        }

        public async Task<Rule?> GetByIdAsync(int id)
        {
            return await _ruleRepo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Rule rule)
        {
            await _ruleRepo.UpdateAsync(rule);
        }
    }
}
