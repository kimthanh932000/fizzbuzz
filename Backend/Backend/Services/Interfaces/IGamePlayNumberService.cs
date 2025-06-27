namespace Backend.Services.Interfaces
{
    public interface IGamePlayNumberService
    {
        Task<int> GenerateRandomNumber(int gamePlayId);
        Task<bool> IsNumberAlreadyUsed(int number, int gamePlayId);
    }
}
