namespace ZBRA.Challenge.Application.Interfaces
{
    public interface IPasswordValidator
    {
        bool IsValid(string password);
    }
}