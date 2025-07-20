using ZBRA.Challenge.Core.Interfaces;

namespace ZBRA.Challenge.Core.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private const string CMD_ADDR = "20";
        private const string CMD_JUMP = "5";

        public ICommand CreateCommand(string cmdTxt)
        {
            // valida entrada
            if (string.IsNullOrWhiteSpace(cmdTxt))
            {
                // esse erro nunca deveria acontecer se o parser estiver ok
                throw new ArgumentException("Texto do comando não pode ser vazio!", nameof(cmdTxt));
            }

            // TODO: usar switch se tiver mais comandos

            if (cmdTxt.StartsWith(CMD_ADDR))
            {
                var valor = cmdTxt.Length > 2
                    ? int.Parse(cmdTxt[2..])
                    : 0;

                return new AddressCommand(valor);
            }
            else if (cmdTxt.StartsWith(CMD_JUMP))
            {
                int pulo;
                if (cmdTxt.Length > 1)
                    pulo = int.Parse(cmdTxt.Substring(1));
                else
                    pulo = 1;

                return new JumpCommand(pulo);
            }
            else
            {
                return new NoOpCommand();
            }
        }

        /* possível melhoria futura:
        private int TryParseValue(string text, int defaultValue)
        {
            if (int.TryParse(text, out int result))
                return result;
            return defaultValue;
        }
        */
    }
}