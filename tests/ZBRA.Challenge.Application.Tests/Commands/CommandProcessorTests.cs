using Moq;
using Xunit;
using System.Collections.Generic;
using ZBRA.Challenge.Application.Commands;
using ZBRA.Challenge.Core.Interfaces;

namespace ZBRA.Challenge.Application.Tests.Commands
{
    /// <summary>
    /// Testes unitários para o CommandProcessor
    /// </summary>
    public class CommandProcessorTests
    {
        // TODO: Adicionar mais testes para casos complexos de processamento em cadeia

        [Fact]
        public void ProcessCommands_WithEmptyCommandList_ReturnsZero()
        {
            // Arrange
            var comandos = new List<string>();
            var mockFactory = new Mock<ICommandFactory>();
            var processor = new CommandProcessor(comandos, mockFactory.Object);

            // Act
            int resultado = processor.ProcessCommands();

            // Assert
            Assert.Equal(0, resultado);
            // Garante que factory nunca foi chamado com uma lista vazia
            mockFactory.Verify(f => f.CreateCommand(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void ProcessCommands_WithSingleIncrementCommand_ReturnsIncrement()
        {
            // Arrange
            var comandos = new List<string> { "201" };
            var mockFactory = new Mock<ICommandFactory>();
            var mockComando = new Mock<ICommand>();

            // Configura o comando para incrementar o endereço em 1
            mockComando.Setup(c => c.Execute(0)).Returns((1, 1));
            mockFactory.Setup(f => f.CreateCommand("201")).Returns(mockComando.Object);

            var processor = new CommandProcessor(comandos, mockFactory.Object);

            // Act
            int resultado = processor.ProcessCommands();

            // Assert
            Assert.Equal(1, resultado);
            mockFactory.Verify(f => f.CreateCommand("201"), Times.Once());
            mockComando.Verify(c => c.Execute(0), Times.Once());

            // Verificação adicional (opcional)
            //Assert.Equal(1, processor.CommandExecutionCount);
        }

        [Fact]
        public void ProcessCommands_WithJumpCommand_SkipsCommandsCorrectly()
        {
            // Arrange
            var comandos = new List<string> { "52", "201", "202" };
            var mockFactory = new Mock<ICommandFactory>();
            var mockJump = new Mock<ICommand>();
            var mockIncrement = new Mock<ICommand>();

            // "52" pula para o índice 2 (ignora o "201")
            mockJump.Setup(c => c.Execute(0)).Returns((2, 0));
            mockFactory.Setup(f => f.CreateCommand("52")).Returns(mockJump.Object);

            // "202" incrementa o endereço em 2 e finaliza
            mockIncrement.Setup(c => c.Execute(2)).Returns((3, 2));
            mockFactory.Setup(f => f.CreateCommand("202")).Returns(mockIncrement.Object);

            var processor = new CommandProcessor(comandos, mockFactory.Object);

            // Act
            int resultado = processor.ProcessCommands();

            // Assert
            Assert.Equal(2, resultado);
            mockFactory.Verify(f => f.CreateCommand("52"), Times.Once());
            mockFactory.Verify(f => f.CreateCommand("202"), Times.Once());
            // Verifica que o comando do meio nunca foi executado
            mockFactory.Verify(f => f.CreateCommand("201"), Times.Never());

        }

        [Fact]
        public void ProcessCommands_WithInvalidCommand_DoesNotChangeAddress()
        {
            // Arrange
            var comandos = new List<string> { "999" };
            var mockFactory = new Mock<ICommandFactory>();
            var mockInvalido = new Mock<ICommand>();

            // Comando inválido não altera o endereço e avança normalmente
            mockInvalido.Setup(c => c.Execute(0)).Returns((1, 0));
            mockFactory.Setup(f => f.CreateCommand("999")).Returns(mockInvalido.Object);

            var processor = new CommandProcessor(comandos, mockFactory.Object);

            // Act
            int resultado = processor.ProcessCommands();

            // Assert
            Assert.Equal(0, resultado);
            mockFactory.Verify(f => f.CreateCommand("999"), Times.Once());
            mockInvalido.Verify(c => c.Execute(0), Times.Once());
        }

        /* Teste adicional para implementar depois
        [Fact(Skip = "Implementar quando corrigirmos o bug #123")]
        public void ProcessCommands_WithInfiniteLoop_ThrowsException()
        {
            // TODO: Implementar teste que verifica se detectamos loops infinitos
        }
        */

        // Método auxiliar para verificar a execução de comandos múltiplos
        /*
        private void VerificaExecucaoComandos(List<string> comandos, int enderecoEsperado)
        {
            var factory = new CommandFactory();
            var processor = new CommandProcessor(comandos, factory);
            int resultado = processor.ProcessCommands();
            Assert.Equal(enderecoEsperado, resultado);
        }
        */
    }
}