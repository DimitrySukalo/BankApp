using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.DAL.Entity;
using BankApp.DAL.Models;
using BankApp.PL.ViewModels;
using BankApp.WEB.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace BankApp.Tests.Tests
{
    /// <summary>
    /// Change country controller tests
    /// </summary>
    public class ChangeCountryControllerTests
    {
        [Fact]
        private void ChangeCountryGetMethodReturnsView()
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
            var userMock = new Mock<DbSet<User>>();
            var countryMock = new Mock<DbSet<Country>>();
            var dbMock = new Mock<BankContext>();

            var users = GetUsers().AsQueryable().BuildMock();
            userMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Object.Provider);
            userMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Object.Expression);
            userMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.Object.ElementType);
            userMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.Object.GetEnumerator());

            var countries = GetCountries().AsQueryable().BuildMock();
            countryMock.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(countries.Object.Provider);
            countryMock.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(countries.Object.Expression);
            countryMock.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(countries.Object.ElementType);
            countryMock.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(countries.Object.GetEnumerator());

            dbMock.Setup(m => m.Users).Returns(userMock.Object);
            dbMock.Setup(m => m.Countries).Returns(countryMock.Object);

            var changeCountryController = new ChangeCountryController(settingMock.Object, mockUserManager.Object, dbMock.Object)
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
            var result = changeCountryController.ChangeCountry().Result as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("ChangeCountry", result?.ViewName);
        }

        [Fact]
        private void ChangeCountryReturnsBadModelState()
        {
            //Arrange
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            var dbMock = new Mock<BankContext>();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
            }));

            mockUserManager.Setup(x => x.GetUserAsync(user)).ReturnsAsync(GetUser());
            var settingMock = new Mock<ISettingService>();
            var changeDataViewModel = new ChangeDataViewModel()
            {
                Country = new Country()
                {
                    City = "1",
                    CountryName = "1"
                }
            };

            var changeCountryController = new ChangeCountryController(settingMock.Object, mockUserManager.Object, dbMock.Object);

            //Act
            var result = changeCountryController.ChangeCountry(changeDataViewModel).Result as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("ChangeCountry", result?.ViewName);
        }

        private User GetUser()
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

        private List<User> GetUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = "1",
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "380111111111",
                    Country = new Country()
                    {
                        Id = 1,
                        City = "test",
                        CountryName = "test",
                        UserForeignKey = "1"
                    }
                },
                new User()
                {
                    Id = "2",
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "380111111111",
                    Country = new Country()
                    {
                        Id = 2,
                        City = "test",
                        CountryName = "test",
                        UserForeignKey = "2"
                    }
                },
                new User()
                {
                    Id = "3",
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "380111111111",
                    Country = new Country()
                    {
                        Id = 3,
                        City = "test",
                        CountryName = "test",
                        UserForeignKey = "3"
                    }
                },
                new User()
                {
                    Id = "4",
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "380111111111",
                    Country = new Country()
                    {
                        Id = 4,
                        City = "test",
                        CountryName = "test",
                        UserForeignKey = "4"
                    }
                }
            };
        }

        private List<Country> GetCountries()
        {
            return new List<Country>()
            {
                new Country()
                {
                    Id = 1,
                    City = "test",
                    CountryName = "test",
                    UserForeignKey = "1"
                },
                new Country()
                {
                    Id = 2,
                    City = "test",
                    CountryName = "test",
                    UserForeignKey = "2"
                },
                new Country()
                {
                    Id = 3,
                    City = "test",
                    CountryName = "test",
                    UserForeignKey = "3"
                },
                new Country()
                {
                    Id = 4,
                    City = "test",
                    CountryName = "test",
                    UserForeignKey = "4"
                }
            };
        }
    }
}
