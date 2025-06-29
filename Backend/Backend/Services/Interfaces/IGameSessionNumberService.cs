namespace Backend.Services.Interfaces
{
    public interface IGameSessionNumberService
    {
        int GenerateRandomNumber(int gameSessionId);
        Task<bool> IsNumberUsed(int number, int gameSessionId);
    }
}
