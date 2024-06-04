using API.Models;
using API.Services;
using API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;


namespace UnitTest.Services.TokenServicesTest
{
    [TestFixture]
    internal class CustomerTokenServiceTest
    {
        private ITokenService<Customer> _customerTokenService;

        [SetUp]
        public void Setup()
        {
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x["JWT:CustomerSecret"]).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            _customerTokenService = new CustomerTokenService(configuration.Object);
        }

        [Test]
        public void GenerateToken_WhenCalled_ReturnsToken()
        {
            // Arrange
            Customer customer = new Customer
            {
                CustomerId = 1,
                CustomerName = "Mani",
                CustomerEmail = "mani@gmail.com",
                CustomerPhone = "123456789"
            };

            // Act
            var result = _customerTokenService.GenerateToken(customer);

            // Assert
            Assert.IsNotNull(result);
         
        }
    }
}
