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
        private void RegisterUserReturnsViewResultWithRegisterModel()
        {
            //Arrange
            var serviceMock = new Mock<IRegisterService>();
            var emailMock = new Mock<ISendEmailService>();
            var registerController = new RegisterController(serviceMock.Object, emailMock.Object);
            RegisterViewModel registerViewModel = null;


            //Act
            var result = registerController.RegisterAccount(registerViewModel) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Register", result?.ViewName);
        }

        [Fact]
        private void ConfirmEmailReturnsUserIsNull()
        {
            //Arrange
            var serviceMock = new Mock<IRegisterService>();
            var emailMock = new Mock<ISendEmailService>();
            var registerController = new RegisterController(serviceMock.Object, emailMock.Object);

            string userId = null;
            string code = null;

            //Act
            var result = registerController.ConfirmEmail(userId, code) as ContentResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Error", result?.Content);
        }
    }
}
