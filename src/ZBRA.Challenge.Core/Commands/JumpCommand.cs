using ZBRA.Challenge.Core.Interfaces;

namespace ZBRA.Challenge.Core.Commands
{
    public class JumpCommand : ICommand
    {
        private readonly int _jump;

        // TODO: adicionar validação para não permitir valores negativos muito grandes

        public JumpCommand(int jumpValue)
        {
            _jump = jumpValue;
        }

        public (int NextCommandIndex, int AddressModification) Execute(int currentIndex)
        {
            return (currentIndex + _jump, 0);
        }

        /* 
        // Método auxiliar para verificar se vai sair dos limites
        private bool EstaDentroLimites(int index, int max)
        {
            return index >= 0 && index < max;
        }
        */
    }
}