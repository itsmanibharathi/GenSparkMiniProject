using API.Models;
using API.Services;
using API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;


namespace UnitTest.Services.TokenServicesTest
{
    [TestFixture]
    internal class EmployeeTokenServiceTest
    {
        private ITokenService<Employee> _employeeTokenService;

        [SetUp]
        public void Setup()
        {
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x["Jwt:EmployeeSecret"]).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            _employeeTokenService = new EmployeeTokenService(configuration.Object);
        }

        [Test]
        public void GenerateToken_WhenCalled_ReturnsToken()
        {
            // Arrange
            Employee employee = new Employee
            {
                EmployeeId = 1,
                EmployeeName = "Mani",
            };

            // Act
            var result = _employeeTokenService.GenerateToken(employee);

            // Assert
            Assert.IsNotNull(result);
         
        }
    }
}
