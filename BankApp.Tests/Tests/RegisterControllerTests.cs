using BankApp.WEB.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BankApp.Tests.Tests
{
    public class RegisterControllerTests
    {
        [Fact]
        private void IndexViewTests()
        {
            //Arrange
            var registerController = new RegisterController();

            //Act
            var result = registerController.Index() as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Register", result?.ViewName);
        }
    }
}
