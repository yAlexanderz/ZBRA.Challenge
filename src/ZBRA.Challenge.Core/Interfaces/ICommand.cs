namespace ZBRA.Challenge.Core.Interfaces
{
    public interface ICommand
    {
        (int NextCommandIndex, int AddressModification) Execute(int currentIndex);
    }
}