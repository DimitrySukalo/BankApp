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
            var resultOfMocks = GetMocks();

            var users = GetUsers().AsQueryable().BuildMock();
            SetSettingsInDb(resultOfMocks.userMock, users);

            var countries = GetCountries().AsQueryable().BuildMock();
            SetSettingsInDb(resultOfMocks.countryMock, countries);

            resultOfMocks.dbMock.Setup(m => m.Users).Returns(resultOfMocks.userMock.Object);
            resultOfMocks.dbMock.Setup(m => m.Countries).Returns(resultOfMocks.countryMock.Object);

            var changeCountryController = new ChangeCountryController(resultOfMocks.settingMock.Object, resultOfMocks.userManager.Object, resultOfMocks.dbMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = resultOfMocks.user
                    }
                }
            };

            //Act
            var result = changeCountryController.ChangeCountry().Result as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("ChangeCountry", result?.ViewName);
        }

        private static (Mock<UserManager<User>> userManager, Mock<ISettingService> settingMock,
                        Mock<DbSet<User>> userMock, Mock<DbSet<Country>> countryMock, Mock<BankContext> dbMock, ClaimsPrincipal user) GetMocks()
        {
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

            return (mockUserManager, settingMock, userMock, countryMock, dbMock, user);
        }

        [Fact]
        private void ChangeCountryReturnsBadModelState()
        {
            //Arrange
            var resultOfMocks = GetMocks();
            var changeDataViewModel = new ChangeDataViewModel()
            {
                Country = new Country()
                {
                    City = "1",
                    CountryName = "1"
                }
            };

            var changeCountryController = new ChangeCountryController(resultOfMocks.settingMock.Object, resultOfMocks.userManager.Object, resultOfMocks.dbMock.Object);

            //Act
            var result = changeCountryController.ChangeCountry(changeDataViewModel).Result as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("ChangeCountry", result?.ViewName);
        }

        private static void SetSettingsInDb<T>(Mock<DbSet<T>> mock, Mock<IQueryable<T>> items) where T : class
        {
            mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(items.Object.Provider);
            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(items.Object.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(items.Object.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(items.Object.GetEnumerator());
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

        private static List<User> GetUsers()
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

        private static List<Country> GetCountries()
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
