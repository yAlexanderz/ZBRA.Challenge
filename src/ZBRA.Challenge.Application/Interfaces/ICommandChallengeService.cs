namespace ZBRA.Challenge.Application.Interfaces
{
    public interface ICommandChallengeService
    {
        Task<(int Address, int CommandCount)> SolveAsync(string commandsFilePath);
    }
}
