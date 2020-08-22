using BankApp.BLL.Interfaces;
using BankApp.PL.ViewModels;
using BankApp.WEB.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BankApp.Tests.Tests
{
    public class RegisterControllerTests
    {
        [Fact]
        private void IndexViewTests()
        {
            //Arrange
            var mock = new Mock<IRegisterService>();
            var registerController = new RegisterController(mock.Object);

            //Act
            var result = registerController.Index() as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Register", result?.ViewName);
        }

        [Fact]
        private void RegisterUserReturnsViewResultWithRegisterModel()
        {
            //Arrange
            var registerMock = new Mock<IRegisterService>();
            var registerController = new RegisterController(registerMock.Object);
            RegisterViewModel registerViewModel = null;


            //Act
            var result = registerController.RegisterAccount(registerViewModel) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Register", result?.ViewName);
        }
    }
}
