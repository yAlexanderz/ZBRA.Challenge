using Moq;
using Xunit;
using System.Collections.Generic;
using ZBRA.Challenge.Application.Password;
using ZBRA.Challenge.Core.Interfaces;

namespace ZBRA.Challenge.Application.Tests.Password
{
    /// <summary>
    /// Testes unitários para o validador de senhas
    /// </summary>
    public class PasswordValidatorTests
    {

        [Fact]
        public void IsValid_AllRulesPassing_ReturnsTrue()
        {
            // Arrange
            var mockRule1 = new Mock<IPasswordRule>();
            var mockRule2 = new Mock<IPasswordRule>();
            // Configura as regras para retornarem válido
            mockRule1.Setup(r => r.Validate(It.IsAny<string>())).Returns(true);
            mockRule2.Setup(r => r.Validate(It.IsAny<string>())).Returns(true);

            var regras = new List<IPasswordRule> { mockRule1.Object, mockRule2.Object };
            var validador = new PasswordValidator(regras);

            // Act - testa com uma senha qualquer
            var resultado = validador.IsValid("123456");

            // Assert
            Assert.True(resultado);
            // Verifica que ambas as regras foram chamadas exatamente uma vez
            mockRule1.Verify(r => r.Validate("123456"), Times.Once);
            mockRule2.Verify(r => r.Validate("123456"), Times.Once);
        }

        [Fact]
        public void IsValid_OneRuleFailing_ReturnsFalse()
        {
            // Arrange
            var mockRule1 = new Mock<IPasswordRule>();
            var mockRule2 = new Mock<IPasswordRule>();
            mockRule1.Setup(r => r.Validate(It.IsAny<string>())).Returns(true);
            mockRule2.Setup(r => r.Validate(It.IsAny<string>())).Returns(false); // Esta regra falha

            var regras = new List<IPasswordRule> { mockRule1.Object, mockRule2.Object };
            var validador = new PasswordValidator(regras);

            // Act
            var resultado = validador.IsValid("123456");

            // Assert
            Assert.False(resultado); // Deve falhar pois uma regra falhou
            mockRule1.Verify(r => r.Validate("123456"), Times.Once);
            mockRule2.Verify(r => r.Validate("123456"), Times.Once);
        }

        [Fact]
        public void IsValid_FirstRuleFailing_DoesNotCheckSecondRule()
        {
            // Arrange - configura a primeira regra para falhar
            var mockRule1 = new Mock<IPasswordRule>();
            var mockRule2 = new Mock<IPasswordRule>();
            mockRule1.Setup(r => r.Validate(It.IsAny<string>())).Returns(false);

            var regras = new List<IPasswordRule> { mockRule1.Object, mockRule2.Object };
            var validador = new PasswordValidator(regras);

            // Act
            var resultado = validador.IsValid("123456");

            // Assert
            Assert.False(resultado); // Deve falhar na primeira regra
            mockRule1.Verify(r => r.Validate("123456"), Times.Once);
            // Verifica que a segunda regra nunca foi chamada (curto-circuito)
            mockRule2.Verify(r => r.Validate(It.IsAny<string>()), Times.Never);

            // TODO: considerar adicionar teste com mais de duas regras
        }

        /* Teste adicional que poderia ser implementado:
        [Fact]
        public void IsValid_WithEmptyRulesList_ReturnsTrue()
        {
            // Valida o comportamento com lista vazia de regras
            var validador = new PasswordValidator(new List<IPasswordRule>());
            Assert.True(validador.IsValid("qualquer_senha"));
        }
        */
    }
}