using BankApp.BLL.DTO;
using BankApp.BLL.Services;
using BankApp.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BankApp.Tests.Tests
{
    /// <summary>
    /// Login service tests
    /// </summary>
    public class LoginServiceTests
    {
        [Fact]
        private void SignInUserReturnFail()
        {
            //Arrange
            var service = new Mock<IUnitOfWork>();
            var logger = new Mock<ILogger<LoginService>>();
            var loginService = new LoginService(service.Object, logger.Object);

            //Act
            var result = loginService.SignInUserAccount(null);


            //Assert
            Assert.NotNull(result);
            Assert.False(result?.Successed);
        }
    }
}
