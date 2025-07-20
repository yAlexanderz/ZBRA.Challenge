using ZBRA.Challenge.Core.Interfaces;

namespace ZBRA.Challenge.Application.Commands
{
    public class CommandProcessor
    {
        private readonly IList<string> _cmds;
        private readonly ICommandFactory _factory;

        public CommandProcessor(IList<string> commands, ICommandFactory cmdFactory)
        {
            _cmds = commands;
            _factory = cmdFactory;
        }

        public int ProcessCommands()
        {
            var address = 0;
            var index = 0;

            while (index >= 0 && index < _cmds.Count)
            {
                var cmdTxt = _cmds[index];
                var cmd = _factory.CreateCommand(cmdTxt);

                var (nextCmdIndex, addressMod) = cmd.Execute(index);

                address += addressMod;
                index = nextCmdIndex;
            }

            return address;
        }
    }
}