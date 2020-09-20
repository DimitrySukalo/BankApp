using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.PL.ViewModels;
using BankApp.WEB.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Security.Claims;
using Xunit;

namespace BankApp.Tests.Tests
{
    /// <summary>
    /// Change phone controller tests
    /// </summary>
    public class ChangePhoneControllerTests
    {
        [Fact]
        private void ChangePhoneMethodReturnsView()
        {
            //Arrange
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
            }));

            mockUserManager.Setup(x => x.GetUserAsync(user)).ReturnsAsync(GetUser());
            var settingMock = new Mock<ISettingService>();
            var changePhoneController = new ChangePhoneController(settingMock.Object, mockUserManager.Object);

            var rnd = new Random();
            var phone = rnd.Next(0, 9999999).ToString();
            var changeDataViewModel = new ChangeDataViewModel()
            {
                PhoneNumber = phone
            };

            //Act
            var result = changePhoneController.ChangePhone(changeDataViewModel).Result as ViewResult;
            var model = result?.Model as ChangeDataViewModel;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(phone, model.PhoneNumber);
            Assert.Equal("ChangePhone", result?.ViewName);
        }

        private User GetUser()
        {
            return new User()
            {
                FirstName = "test",
                LastName = "test",
                Email = "test@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "380111111111"
            };
        }

        [Fact]
        private void ChangePhoneReturnsViewPage()
        {
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
            }));

            mockUserManager.Setup(x => x.GetUserAsync(user)).ReturnsAsync(GetUser());
            var settingMock = new Mock<ISettingService>();
            var changePhoneController = new ChangePhoneController(settingMock.Object, mockUserManager.Object)
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
            var result = changePhoneController.ChangePhone().Result as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("ChangePhone", result?.ViewName);
        }
    }
}
