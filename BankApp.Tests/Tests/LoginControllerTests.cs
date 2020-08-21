using BankApp.WEB.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BankApp.Tests.Tests
{
    public class LoginControllerTests
    {
        [Fact]
        private void IndexViewTests()
        {
            //Arrange
            var loginController = new LoginController();

            //Act
            var result = loginController.Index() as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Login", result.ViewName);
        }
    }
}
