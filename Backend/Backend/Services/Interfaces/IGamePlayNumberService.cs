namespace Backend.Services.Interfaces
{
    public interface IGamePlayNumberService
    {
        Task GenerateRandomNumber(int gamePlayId);
    }
}
