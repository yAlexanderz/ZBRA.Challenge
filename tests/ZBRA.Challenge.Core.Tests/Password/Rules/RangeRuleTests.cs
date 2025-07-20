using System;
using Xunit;
using ZBRA.Challenge.Core.Password.Rules;

namespace ZBRA.Challenge.Core.Tests.Password.Rules
{
    public class RangeRuleTests
    {
        [Theory]
        [InlineData("200000", true)]  // Dentro do intervalo
        [InlineData("184759", true)]  // Limite inferior
        [InlineData("856920", true)]  // Limite superior
        [InlineData("100000", false)] // Abaixo do intervalo
        [InlineData("900000", false)] // Acima do intervalo
        [InlineData("abc", false)]    // Não é um número
        public void Validate_WithVariousInputs_ReturnsExpectedResults(string password, bool expected)
        {
            // Arrange
            var rule = new RangeRule(184759, 856920);

            // Act
            bool result = rule.Validate(password);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}