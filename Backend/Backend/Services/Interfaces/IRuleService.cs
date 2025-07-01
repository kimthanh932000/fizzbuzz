namespace Backend.Services.Interfaces
{
    public interface IRuleService
    {
        Task<IEnumerable<Rule>> GetByGameIdAsync(int gameId);
        Task<Rule?> GetByIdAsync(int id);
        Task<Rule> AddAsync(Rule rule);
        Task UpdateAsync(Rule rule);
        Task DeleteAsync(int id);
        Task<IEnumerable<Rule>> AddRulesAsync(List<Rule> rules);
    }
}
