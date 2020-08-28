using BankApp.BLL.Services;
using BankApp.DAL.Interfaces;
using Moq;
using Xunit;

namespace BankApp.Tests.Tests
{
    /// <summary>
    /// Register service tests
    /// </summary>
    public class RegisterServiceTests
    {
        [Fact]
        private void RegisterNullUserDTO()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            var registerService = new RegisterService(mock.Object);

            //Act
            var result = registerService.RegisterUser(null);

            //Assert
            Assert.Null(result);
        }
    }
}
