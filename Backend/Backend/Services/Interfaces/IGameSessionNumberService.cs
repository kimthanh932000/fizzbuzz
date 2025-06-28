namespace Backend.Services.Interfaces
{
    public interface IGameSessionNumberService
    {
        Task<int> GenerateRandomNumber(int gameSessionId);
        Task<bool> IsNumberAlreadyUsed(int number, int gameSessionId);
    }
}
