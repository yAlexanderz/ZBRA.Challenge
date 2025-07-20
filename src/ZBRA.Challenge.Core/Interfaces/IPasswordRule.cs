namespace ZBRA.Challenge.Core.Interfaces
{
    public interface IPasswordRule
    {
        bool Validate(string password);
    }
}