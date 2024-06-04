using API.Models;
using API.Services;
using API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;


namespace UnitTest.Services.TokenServicesTest
{
    [TestFixture]
    internal class RestaurantTokenServiceTest
    {
        private ITokenService<Restaurant> _restaurantTokenService;

        [SetUp]
        public void Setup()
        {
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x["Jwt:RestaurantSecret"]).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            _restaurantTokenService = new RestaurantTokenService(configuration.Object);
        }

        [Test]
        public void GenerateToken_WhenCalled_ReturnsToken()
        {
            // Arrange
            Restaurant restaurant = new Restaurant
            {
                RestaurantId = 1,
                Name= "KFC"
            };

            // Act
            var result = _restaurantTokenService.GenerateToken(restaurant);

            // Assert
            Assert.IsNotNull(result);
         
        }
    }
}
