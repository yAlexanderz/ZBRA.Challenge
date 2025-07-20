using Xunit;
using ZBRA.Challenge.Core.Password.Rules;

namespace ZBRA.Challenge.Core.Tests.Password.Rules
{
    public class ExactlyTwoAdjacentDigitsRuleTests
    {
        [Theory]
        [InlineData("112233", true)]  // Múltiplos pares exatos
        [InlineData("123455", true)]  // Um par exato no final
        [InlineData("111222", false)] // Nenhum par exato, apenas trincas
        [InlineData("123444", false)] // Trinca, sem par exato
        [InlineData("112345", true)]  // Par exato no início
        [InlineData("444557", true)]  // Contém um par exato (55) entre outras repetições
        public void Validate_WithVariousInputs_ReturnsExpectedResults(string password, bool expected)
        {
            // Arrange
            var rule = new ExactlyTwoAdjacentDigitsRule();

            // Act
            bool result = rule.Validate(password);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}