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

        public async Task DeleteAsync(int id)
        {
            var entity = await _ruleRepo.GetByIdAsync(id);
            if (entity != null)
            {
                await _ruleRepo.DeleteAsync(entity);
            }
        }

        public async Task<IEnumerable<Rule>> GetAllAsync()
        {
            return await _ruleRepo.GetAllAsync();
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
