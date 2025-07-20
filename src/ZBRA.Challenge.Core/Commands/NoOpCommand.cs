using ZBRA.Challenge.Core.Interfaces;

namespace ZBRA.Challenge.Core.Commands
{
    // Comando que não faz nada (apenas avança pro próximo)
    public class NoOpCommand : ICommand
    {

        /* 
        * NoOp = No Operation
        * Não modifica o endereço, só avança para o próximo comando
        * Útil como placeholder ou para pausas
        */

        public (int NextCommandIndex, int AddressModification) Execute(int currentIndex)
        {

            return (currentIndex + 1, 0);
        }

        // TODO: implementar ToString() pra debug
        // public override string ToString() => "NOP";
    }
}