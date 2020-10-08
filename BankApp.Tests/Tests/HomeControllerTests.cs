using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.DAL.Entity;
using BankApp.DAL.Models;
using BankApp.PL.ViewModels;
using BankApp.WEB.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BankApp.Tests.Tests
{
    /// <summary>
    /// Home controller tests
    /// </summary>
    public class HomeControllerTests
    {
        [Fact]
        private async Task HomeIndexMethodReturnsView()
        {
            //Arrange
            var resultOfMocks = GetMocks();
            var homeController = new HomeController(resultOfMocks.homeServiceMock.Object, resultOfMocks.userManager.Object);

            //Act
            var result = await homeController.Index() as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result?.ViewName);
        }

        [Fact]
        private async Task SendMessageMethodReturnBadModelState()
        {
            //Arrange
            var resultOfMocks = GetMocks();
            var homeController = new HomeController(resultOfMocks.homeServiceMock.Object, resultOfMocks.userManager.Object);

            var multiplyHomeModels = new MultiplyHomeModels()
            {
                MessageViewModel = null,
                User = null
            };

            //Act
            var result = await homeController.SendMessage(multiplyHomeModels) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result?.ViewName);
        }

        [Fact]
        private async Task SendMessageMethodReturnsBadRequestContent()
        {
            //Arrange 
            var resultOfMocks = GetMocks();
            var dbMock = new Mock<BankContext>();
            var userMessageDTOMock = new Mock<UserMessageDTO>();
            resultOfMocks.homeServiceMock.Setup(m => m.SaveUserMessageInDbAsync(userMessageDTOMock.Object)).ReturnsAsync(new OperationSuccessed("Message is not saved", false));
            var homeController = new HomeController(resultOfMocks.homeServiceMock.Object, resultOfMocks.userManager.Object);

            var multiplyHomeModels = new MultiplyHomeModels()
            {
                MessageViewModel = new UserMessageViewModel(),
                User = null
            };

            //Act
            var result = await homeController.SendMessage(multiplyHomeModels) as ContentResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Bad request", result?.Content);
        }

        private static (Mock<UserManager<User>> userManager, Mock<IHomeService> homeServiceMock, ClaimsPrincipal user) GetMocks()
        {
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
            }));

            mockUserManager.Setup(x => x.GetUserAsync(user)).ReturnsAsync(GetUser());
            var mockService = new Mock<IHomeService>();

            return (mockUserManager, mockService, user);
        }

        private static User GetUser()
        {
            return new User()
            {
                Id = "1",
                FirstName = "test",
                LastName = "test",
                Email = "test@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "380111111111"
            };
        }
    }
}
