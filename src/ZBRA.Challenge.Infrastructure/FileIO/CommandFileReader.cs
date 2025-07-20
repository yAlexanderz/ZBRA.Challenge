using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ZBRA.Challenge.Infrastructure.FileIO
{
    /// <summary>
    /// Leitor de arquivos de comando
    /// </summary>
    public class CommandFileReader
    {
        // Cache de comandos para evitar releituras do mesmo arquivo
        //private static Dictionary<string, List<string>> _commandCache = new Dictionary<string, List<string>>();

        /// <summary>
        /// Lê comandos de um arquivo de forma assíncrona
        /// </summary>
        /// <param name="filePath">Caminho do arquivo a ser lido</param>
        /// <returns>Lista de comandos encontrados no arquivo</returns>
        public async Task<List<string>> ReadCommandsAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Caminho do arquivo não pode ser vazio", nameof(filePath));

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Arquivo de comandos não encontrado: {filePath}", filePath);
            }

            /* Versão com cache
            if (_commandCache.TryGetValue(filePath, out var cachedCommands))
            {
                Console.WriteLine($"Usando cache para {filePath}");
                return cachedCommands;
            }
            */

            var commands = new List<string>();

            using (var reader = new StreamReader(filePath))
            {
                string? linha;

                while ((linha = await reader.ReadLineAsync()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(linha))
                    {
                        commands.Add(linha.Trim());

                    }
                }
            }

            // Guarda no cache para uso futuro
            //_commandCache[filePath] = commands;

            return commands;

            /* Possível melhoria: criar versão síncrona também
            public List<string> ReadCommands(string filePath) {
                return ReadCommandsAsync(filePath).GetAwaiter().GetResult();
            }
            */
        }
    }
}