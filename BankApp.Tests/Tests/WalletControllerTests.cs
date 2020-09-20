using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.DAL.Entity;
using BankApp.WEB.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

namespace BankApp.Tests.Tests
{
    /// <summary>
    /// Wallet controller tests
    /// </summary>
    public class WalletControllerTests
    {
        [Fact]
        private void AddWalletReturnView()
        {
            //Arrange
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
            }));

            mockUserManager.Setup(x => x.GetUserAsync(user)).ReturnsAsync(GetUser());
            var serviceMock = new Mock<IWalletService>();
            var dbMock = new Mock<BankContext>();
            var walletController = new WalletController(serviceMock.Object, mockUserManager.Object, dbMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = user
                    }
                }
            };

            //Act
            var result = walletController.AddWallet().Result as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("AddWallet", result?.ViewName);
        }

        private User GetUser()
        {
            return new User()
            {
                FirstName = "test",
                LastName = "test",
                Email = "test@gmail.com",
                EmailConfirmed = true,
            };
        }
    }
}
