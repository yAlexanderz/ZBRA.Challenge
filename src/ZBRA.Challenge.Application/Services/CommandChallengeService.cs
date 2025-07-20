using System.Diagnostics;
using ZBRA.Challenge.Application.Commands;
using ZBRA.Challenge.Application.Interfaces;
using ZBRA.Challenge.Core.Commands;
using ZBRA.Challenge.Core.Interfaces;
using ZBRA.Challenge.Infrastructure.FileIO;

namespace ZBRA.Challenge.Application.Services
{
    public class CommandChallengeService : ICommandChallengeService
    {
        private readonly ICommandFactory _cmdFactory;
        private readonly CommandFileReader _reader;

        public CommandChallengeService()
        {
            // TODO: injetar essas dependências via DI ao invés de instanciar aqui (Aplicações mais complexas e maiores)
            _cmdFactory = new CommandFactory();
            _reader = new CommandFileReader();
        }

        public async Task<(int Address, int CommandCount)> SolveAsync(string cmdPath)
        {
            if (string.IsNullOrWhiteSpace(cmdPath) || !File.Exists(cmdPath))
            {
                throw new FileNotFoundException("Arquivo de comandos não encontrado.", cmdPath);
            }

            var commands = await _reader.ReadCommandsAsync(cmdPath);

            // versão inicial com timer pra testar performance
            //var sw = Stopwatch.StartNew();
            var proc = new CommandProcessor(commands, _cmdFactory);
            var address = proc.ProcessCommands();
            //sw.Stop();
            //Console.WriteLine($"Tempo de execução: {sw.ElapsedMilliseconds}ms");

            return (address, commands.Count);
        }
    }
}