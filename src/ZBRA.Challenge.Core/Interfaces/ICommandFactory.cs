namespace ZBRA.Challenge.Core.Interfaces
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(string commandString);
    }
}