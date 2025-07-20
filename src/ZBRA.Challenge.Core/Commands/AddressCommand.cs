using ZBRA.Challenge.Core.Interfaces;

namespace ZBRA.Challenge.Core.Commands
{
    /// <summary>
    /// Comando que incrementa o endereço
    /// </summary>
    public class AddressCommand : ICommand
    {
        private readonly int _incr;

        public AddressCommand(int incrementValue)
        {
            _incr = incrementValue;
        }

        public (int NextCommandIndex, int AddressModification) Execute(int currentIndex)
        {
            return (currentIndex + 1, _incr);
        }

        // TODO: adicionar ToString() para facilitar debug
        //public override string ToString() => $"Add[{_incr}]";
    }
}