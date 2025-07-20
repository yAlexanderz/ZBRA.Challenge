using Microsoft.Extensions.DependencyInjection;
using ZBRA.Challenge.Application.Interfaces;
using ZBRA.Challenge.Application.Services;

namespace ZBRA.Challenge.Console;

class Program
{
    // Limites definidos pelo desafio
    private const int MIN_PASSWD = 184759; // valor min conforme regras
    private const int MAX_PASSWD = 856920; // valor max

    static async Task Main(string[] args)
    {
        // TODO(Implementações mais robustas): Refatorar para usar arquivo de config
        var services = ConfigServices();

        System.Console.WriteLine("====================");
        System.Console.WriteLine("## ZBRA Challenge ##");
        System.Console.WriteLine("====================\n");

        // Pega o path do arquivo de comandos
        var cmdPath = args.Length > 0 && !string.IsNullOrWhiteSpace(args[0])
            ? args[0]
            : GetDefaultCmdPath();

        var pwdService = services.GetRequiredService<IPasswordChallengeService>();
        var cmdService = services.GetRequiredService<ICommandChallengeService>();

        ResolveQ1(pwdService);
        await ResolveQ2(cmdService, cmdPath);

        // Espera tecla só se estiver rodando interativo
        if (!System.Console.IsInputRedirected)
        {
            System.Console.WriteLine("\nAperte qualquer tecla pra sair...");
            System.Console.ReadKey();
        }
    }

    private static ServiceProvider ConfigServices()
    {
        return new ServiceCollection()
            .AddSingleton<IPasswordChallengeService, PasswordChallengeService>()
            .AddSingleton<ICommandChallengeService, CommandChallengeService>()
            .BuildServiceProvider();
    }

    private static string GetDefaultCmdPath()
    {
        string localPath = Path.Combine(Directory.GetCurrentDirectory(), "commands.txt");

        if (File.Exists(localPath))
        {
            System.Console.WriteLine($"Arquivo de comandos encontrado em: {localPath}");
            return localPath;
        }

        System.Console.WriteLine("Digite o caminho para o arquivo commands.txt:");
        string? path = System.Console.ReadLine();

        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
        {
            System.Console.WriteLine("Arquivo inválido ou inexistente!");
            System.Console.WriteLine("Coloque o arquivo commands.txt na mesma pasta do programa e tente novamente.");
            Environment.Exit(1);
        }

        return path!;
    }

    static void ResolveQ1(IPasswordChallengeService pwdService)
    {
        System.Console.WriteLine("\nQuestão 1:");
        System.Console.WriteLine("==========================================");

        var p1 = pwdService.SolvePart1(MIN_PASSWD, MAX_PASSWD);
        System.Console.WriteLine($"Resultado Parte 1: {p1}");

        var p2 = pwdService.SolvePart2(MIN_PASSWD, MAX_PASSWD);
        System.Console.WriteLine($"Resultado Parte 2: {p2}");

        System.Console.WriteLine("==========================================");
    }

    static async Task ResolveQ2(ICommandChallengeService cmdSvc, string filePath)
    {
        System.Console.WriteLine("\nQuestão 2:");
        System.Console.WriteLine("==========================================");

        try
        {
            System.Console.WriteLine($"Lendo comandos de:\n {Path.GetFileName(filePath)}");
            System.Console.WriteLine("\n-----------------");

            var (final, count) = await cmdSvc.SolveAsync(filePath);

            System.Console.WriteLine($"Carregados {count} comandos com sucesso.");
            System.Console.WriteLine("-----------------");
            System.Console.WriteLine($"\nEndereço Final: {final}");

            System.Console.WriteLine("==========================================");
        }
        catch (FileNotFoundException ex)
        {
            System.Console.WriteLine($"Erro: {ex.Message}");
            System.Console.WriteLine("Verifique se o arquivo commands.txt está no local correto.");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Erro inesperado: {ex.Message}");

            // Deixa o stack trace comentado, só descomenta se precisar debugar
            //System.Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
    }
}