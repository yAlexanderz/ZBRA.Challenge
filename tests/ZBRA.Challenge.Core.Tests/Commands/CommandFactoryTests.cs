using Xunit;
using ZBRA.Challenge.Core.Commands;
using System;

namespace ZBRA.Challenge.Core.Tests.Commands
{
    /// <summary>
    /// Testes para a fábrica de comandos
    /// </summary>
    public class CommandFactoryTests
    {
        private readonly CommandFactory _factory = new CommandFactory();

        // TODO: adicionar mais casos de teste com valores extremos

        [Theory]
        [InlineData("201", typeof(AddressCommand))]  // comando de endereço
        [InlineData("2010", typeof(AddressCommand))] // outro comando de endereço
        [InlineData("51", typeof(JumpCommand))]      // comando de pulo
        [InlineData("510", typeof(JumpCommand))]     // outro comando de pulo
        [InlineData("42", typeof(NoOpCommand))]      // comando que não faz nada
        [InlineData("123", typeof(NoOpCommand))]     // outro sem operação
        public void CreateCommand_WithVariousInputs_ReturnsCorrectType(string commandText, Type expectedType)
        {
            // Act - cria o comando baseado no texto
            var comando = _factory.CreateCommand(commandText);

            // Assert - verifica se o tipo retornado é o esperado
            Assert.IsType(expectedType, comando);
        }

        /* Teste que ainda precisa ser implementado
        [Fact]
        public void CreateCommand_WithInvalidInput_ThrowsException()
        {
            // TODO: Implementar teste para entradas inválidas
            // Assert.Throws<FormatException>(() => _factory.CreateCommand("abc"));
        }
        */
    }
}