namespace Backend.Repositories.Interfaces
{
    public interface IRuleRepo : IRepoBase<Rule>
    {
        Task<IEnumerable<Rule>> GetByGameIdAsync(int gameId);
        Task<IEnumerable<Rule>> AddRulesAsync(IEnumerable<Rule> rules);
    }
}