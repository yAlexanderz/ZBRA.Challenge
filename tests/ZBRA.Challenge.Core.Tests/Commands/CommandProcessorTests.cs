using System;
using System.Collections.Generic;
using Xunit;
using ZBRA.Challenge.Application.Commands;
using ZBRA.Challenge.Core.Commands;

namespace ZBRA.Challenge.Application.Tests.Commands
{
    /// <summary>
    /// Testes de integração para o CommandProcessor
    /// </summary>
    public class CommandProcessorIntegrationTests
    {

        [Fact]
        public void ProcessCommands_WithExampleInput_ReturnsExpectedResult()
        {
            // Arrange
            // Sequência de comandos da especificação que deve resultar em endereço 3
            var listaComandos = new List<string>
                {
                    "25",   // primeiro comando (não faz nada)
                    "52",   // comando de salto
                    "53",   // nunca deveria executar por causa do salto anterior
                    "202",  // incrementa endereço +2
                    "54",   // não faz nada 
                    "402",  // comando de incremento
                    "203",  // outro incremento
                    "510",  // salto
                    "201"   // incremento final
                };

            var factoryDeComandos = new CommandFactory();

            var processador = new CommandProcessor(listaComandos, factoryDeComandos);

            // Act
            // Executa o processamento completo
            int enderecoFinal = processador.ProcessCommands();

            // Assert
            // De acordo com a especificação, o resultado deve ser 3
            Assert.Equal(3, enderecoFinal);
        }

        // TODO: adicionar mais casos de teste com outras sequências

        /* Ideia para teste futuro
        [Fact(Skip = "Não implementado ainda")]
        public void ProcessCommands_WithLargeInput_CompletesInReasonableTime()
        {
            // Precisará verificar performance com entradas grandes
            // ...
        }
        */
    }
}