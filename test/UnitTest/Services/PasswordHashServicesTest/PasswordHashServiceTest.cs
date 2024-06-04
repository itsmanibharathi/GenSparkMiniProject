using API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Services.PasswordHashServicesTest
{
    [TestFixture]
    internal class PasswordHashServiceTest
    {
        private PasswordHashService _passwordHashService;

        [SetUp]
        public void Setup()
        {
            _passwordHashService = new PasswordHashService();

        }

        [Test]
        public void HashPassword_WhenCalled_ReturnsHashedPassword()
        {
            // Arrange
            var password = "password";

            // Act
            var result = _passwordHashService.Hash(password);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(password, result);
        }

        [Test]
        public void VerifyPassword_WhenCalledWithCorrectPassword_ReturnsTrue()
        {
            // Arrange
            var password = "password";
            var hashedPassword = _passwordHashService.Hash(password);

            // Act
            var result = _passwordHashService.Verify(password, hashedPassword);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void VerifyPassword_WhenCalledWithIncorrectPassword_ReturnsFalse()
        {
            // Arrange
            var password = "password";
            var hashedPassword = _passwordHashService.Hash(password);

            // Act
            var result = _passwordHashService.Verify("wrongPassword", hashedPassword);

            // Assert
            Assert.IsFalse(result);
        }

    }
}
