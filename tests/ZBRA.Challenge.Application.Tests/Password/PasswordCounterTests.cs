using Moq;
using Xunit;
using System;
using ZBRA.Challenge.Application.Interfaces;
using ZBRA.Challenge.Application.Password;

namespace ZBRA.Challenge.Application.Tests.Password
{
    /// <summary>
    /// Testes unitários para a classe PasswordCounter
    /// </summary>
    public class PasswordCounterTests
    {
        [Fact]
        public void CountValidPasswords_WithAllInvalid_ReturnsZero()
        {
            // Arrange
            int min = 100;
            int max = 102;
            var mockValidator = new Mock<IPasswordValidator>();
            mockValidator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(false);

            var counter = new PasswordCounter(mockValidator.Object, min, max);

            // Act
            int result = counter.CountValidPasswords();

            // Assert
            Assert.Equal(0, result);
            // Verifica que o validador foi chamado exatamente 3 vezes (100, 101, 102)
            mockValidator.Verify(v => v.IsValid(It.IsAny<string>()), Times.Exactly(3));

        }

        [Fact]
        public void CountValidPasswords_WithAllValid_ReturnsCorrectCount()
        {
            // Arrange
            int min = 100;
            int max = 102;
            var mockValidator = new Mock<IPasswordValidator>();
            mockValidator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(true);

            var counter = new PasswordCounter(mockValidator.Object, min, max);

            // Act
            int result = counter.CountValidPasswords();

            // Assert
            Assert.Equal(3, result); // 3 senhas válidas (100, 101, 102)
            mockValidator.Verify(v => v.IsValid(It.IsAny<string>()), Times.Exactly(3));

        }

        [Fact]
        public void CountValidPasswords_WithSomeValid_ReturnsCorrectCount()
        {
            // Arrange
            int min = 100;
            int max = 102;
            var mockValidator = new Mock<IPasswordValidator>();

            // Configura apenas a senha 101 como válida
            mockValidator.Setup(v => v.IsValid("100")).Returns(false);
            mockValidator.Setup(v => v.IsValid("101")).Returns(true);
            mockValidator.Setup(v => v.IsValid("102")).Returns(false);

            var counter = new PasswordCounter(mockValidator.Object, min, max);

            // Act
            int resultado = counter.CountValidPasswords();

            // Assert
            Assert.Equal(1, resultado); // Apenas uma senha válida
            mockValidator.Verify(v => v.IsValid(It.IsAny<string>()), Times.Exactly(3));
        }

        /* TODO: Implementar teste para intervalos maiores com amostragem
        [Fact]
        public void CountValidPasswords_WithLargeRange_UsesAcceptablePerformance()
        {
            // Arrange
            int min = 100000;
            int max = 999999;
            var mockValidator = new Mock<IPasswordValidator>();
            mockValidator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(true);
            
            var counter = new PasswordCounter(mockValidator.Object, min, max);
            
            // Act
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            int result = counter.CountValidPasswords();
            stopwatch.Stop();
            
            // Assert
            Assert.True(stopwatch.ElapsedMilliseconds < 5000);
        }
        */
    }
}