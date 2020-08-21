using BankApp.WEB.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BankApp.Tests.Tests
{
    /// <summary>
    /// Unit tests of home controller
    /// </summary>
    public class HomeControllerTests
    {
        [Fact]
        private void HomePageTest()
        {
            //Arrange
            var homeController = new HomeController();


            //Act
            var result = homeController.Index() as ViewResult;


            //Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result?.ViewName);
        }
    }
}
