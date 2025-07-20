using Xunit;
using ZBRA.Challenge.Core.Password.Rules;

namespace ZBRA.Challenge.Core.Tests.Password.Rules
{
    public class NeverDecreasingRuleTests
    {
        [Theory]
        [InlineData("123456", true)]  // Estritamente crescente
        [InlineData("111111", true)]  // Todos iguais (constante)
        [InlineData("112233", true)]  // Crescente com platôs
        [InlineData("654321", false)] // Decrescente
        [InlineData("123432", false)] // Decrescente no meio
        public void Validate_WithVariousInputs_ReturnsExpectedResults(string password, bool expected)
        {
            // Arrange
            var rule = new NeverDecreasingRule();

            // Act
            bool result = rule.Validate(password);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}