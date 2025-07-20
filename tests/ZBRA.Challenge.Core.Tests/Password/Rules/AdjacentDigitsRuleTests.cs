using Xunit;
using ZBRA.Challenge.Core.Password.Rules;

namespace ZBRA.Challenge.Core.Tests.Password.Rules
{
    public class AdjacentDigitsRuleTests
    {
        [Theory]
        [InlineData("122345", true)]  // Possui dígitos adjacentes
        [InlineData("111111", true)]  // Todos os dígitos iguais
        [InlineData("123456", false)] // Não possui dígitos adjacentes
        [InlineData("12", false)]     // Muito curto, sem adjacentes
        [InlineData("11", true)]      // Todos adjacentes
        public void Validate_WithVariousInputs_ReturnsExpectedResults(string password, bool expected)
        {
            // Arrange
            var rule = new AdjacentDigitsRule();

            // Act
            bool result = rule.Validate(password);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}