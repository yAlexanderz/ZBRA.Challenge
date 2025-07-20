using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using ZBRA.Challenge.Infrastructure.FileIO;

namespace ZBRA.Challenge.Infrastructure.Tests.FileIO
{
    /// <summary>
    /// Testes para o leitor de arquivos de comandos
    /// </summary>
    public class CommandFileReaderTests : IDisposable
    {
        private readonly string _testFilePath = "test_commands.txt";

        /// <summary>
        /// Inicializa cada teste garantindo um ambiente limpo
        /// </summary>
        public CommandFileReaderTests()
        {
            // Limpa qualquer arquivo de teste que possa ter sido deixado
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [Fact]
        public async Task ReadCommandsAsync_WithValidFile_ReturnsAllCommands()
        {
            // Arrange
            var expectedCommands = new[] { "201", "52", "203" };
            await File.WriteAllLinesAsync(_testFilePath, expectedCommands);

            var reader = new CommandFileReader();

            // Act
            var commands = await reader.ReadCommandsAsync(_testFilePath);

            // Assert
            Assert.Equal(expectedCommands.Length, commands.Count);
            for (int i = 0; i < expectedCommands.Length; i++)
            {
                Assert.Equal(expectedCommands[i], commands[i]);
            }
        }

        [Fact]
        public async Task ReadCommandsAsync_WithEmptyFile_ReturnsEmptyList()
        {
            // Arrange
            await File.WriteAllTextAsync(_testFilePath, "");

            var reader = new CommandFileReader();

            // Act
            var commands = await reader.ReadCommandsAsync(_testFilePath);

            // Assert
            Assert.Empty(commands);
        }

        [Fact]
        public async Task ReadCommandsAsync_WithMixedContent_SkipsEmptyLines()
        {
            // Arrange
            var fileContent = new[]
            {
                "201",
                "",
                "   ",
                "52",
                "203"
            };
            await File.WriteAllLinesAsync(_testFilePath, fileContent);

            var reader = new CommandFileReader();

            // Act
            var commands = await reader.ReadCommandsAsync(_testFilePath);

            // Assert
            Assert.Equal(3, commands.Count);
            Assert.Equal("201", commands[0]);
            Assert.Equal("52", commands[1]);
            Assert.Equal("203", commands[2]);
        }

        [Fact]
        public async Task ReadCommandsAsync_WithNonexistentFile_ThrowsFileNotFoundException()
        {
            // Arrange
            var reader = new CommandFileReader();
            string nonExistentFilePath = "non_existent_file.txt";

            // Act & Assert
            await Assert.ThrowsAsync<FileNotFoundException>(async () =>
                await reader.ReadCommandsAsync(nonExistentFilePath));
        }

        [Fact]
        public async Task ReadCommandsAsync_WithWhitespaceInCommands_TrimsWhitespace()
        {
            // Arrange
            var fileContent = new[]
            {
                "  201  ",
                " 52",
                "203 "
            };
            await File.WriteAllLinesAsync(_testFilePath, fileContent);

            var reader = new CommandFileReader();

            // Act
            var commands = await reader.ReadCommandsAsync(_testFilePath);

            // Assert
            Assert.Equal(3, commands.Count);
            Assert.Equal("201", commands[0]);
            Assert.Equal("52", commands[1]);
            Assert.Equal("203", commands[2]);
        }

        /// <summary>
        /// Limpa os recursos após cada teste
        /// </summary>
        public void Dispose()
        {
            // Limpa o arquivo de teste
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }
    }
}