using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.DAL.Entity;
using BankApp.DAL.Interfaces;
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
    /// Controller tests
    /// </summary>
    public class PiggyBankControllerTests
    {
        [Fact]
        private void IndexReturnView()
        {
            //Arrange
            var resultOfMocks = GetMocks();

            var users = GetUsers().AsQueryable().BuildMock();
            SetSettingsInDb(resultOfMocks.userMock, users);

            var piggyBanks = GetPiggyBanks().AsQueryable().BuildMock();
            SetSettingsInDb(resultOfMocks.piggyBankMock, piggyBanks);

            var wallets = GetWallets().AsQueryable().BuildMock();
            SetSettingsInDb(resultOfMocks.walletMock, wallets);

            resultOfMocks.dbMock.Setup(m => m.Users).Returns(resultOfMocks.userMock.Object);
            resultOfMocks.dbMock.Setup(m => m.PiggyBanks).Returns(resultOfMocks.piggyBankMock.Object);
            resultOfMocks.dbMock.Setup(m => m.Wallets).Returns(resultOfMocks.walletMock.Object);

            var piggyBankController = new PiggyBankController(resultOfMocks.userManager.Object, resultOfMocks.dbMock.Object, 
                                                              resultOfMocks.piggyBankService.Object)
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
            var result = piggyBankController.Index().Result as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result?.ViewName);
        }

        [Fact]
        private void WithdrawMethodReturnsBadModelStateWithIndexView()
        {
            //Arrange
            var resultOfMocks = GetMocks();

            var users = GetUsers().AsQueryable().BuildMock();
            SetSettingsInDb(resultOfMocks.userMock, users);

            var piggyBanks = GetPiggyBanks().AsQueryable().BuildMock();
            SetSettingsInDb(resultOfMocks.piggyBankMock, piggyBanks);

            var wallets = GetWallets().AsQueryable().BuildMock();
            SetSettingsInDb(resultOfMocks.walletMock, wallets);

            resultOfMocks.dbMock.Setup(m => m.Users).Returns(resultOfMocks.userMock.Object);
            resultOfMocks.dbMock.Setup(m => m.PiggyBanks).Returns(resultOfMocks.piggyBankMock.Object);
            resultOfMocks.dbMock.Setup(m => m.Wallets).Returns(resultOfMocks.walletMock.Object);

            var piggyBankViewModel = new PiggyBankViewModel();
            var piggyBankController = new PiggyBankController(resultOfMocks.userManager.Object, resultOfMocks.dbMock.Object,
                                                              resultOfMocks.piggyBankService.Object)
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
            var result = piggyBankController.Withdraw(piggyBankViewModel).Result as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result?.ViewName);
        }

        [Fact]
        private void WithdrawMethodReturnsBadRequest()
        {
            //Arrange
            var resultOfMocks = GetMocks();

            var users = GetUsers().AsQueryable().BuildMock();
            SetSettingsInDb(resultOfMocks.userMock, users);

            var piggyBanks = GetPiggyBanks().AsQueryable().BuildMock();
            SetSettingsInDb(resultOfMocks.piggyBankMock, piggyBanks);

            var wallets = GetWallets().AsQueryable().BuildMock();
            SetSettingsInDb(resultOfMocks.walletMock, wallets);

            resultOfMocks.dbMock.Setup(m => m.Users).Returns(resultOfMocks.userMock.Object);
            resultOfMocks.dbMock.Setup(m => m.PiggyBanks).Returns(resultOfMocks.piggyBankMock.Object);
            resultOfMocks.dbMock.Setup(m => m.Wallets).Returns(resultOfMocks.walletMock.Object);

            var piggyBankViewModel = new PiggyBankViewModel()
            {
                CardNumber = "1111111111111111",
                PiggyBankId = 1,
                WithdrawSum = 100
            };

            var piggyBankController = new PiggyBankController(resultOfMocks.userManager.Object, resultOfMocks.dbMock.Object,
                                                              resultOfMocks.piggyBankService.Object)
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
            var result = piggyBankController.Withdraw(piggyBankViewModel).Result as ContentResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Bad request", result?.Content);
        }

        private static (Mock<UserManager<User>> userManager, Mock<IPiggyBankService> piggyBankService, Mock<DbSet<User>> userMock, Mock<DbSet<PiggyBank>> piggyBankMock, 
                        Mock<DbSet<Wallet>> walletMock, Mock<BankContext> dbMock, ClaimsPrincipal user) GetMocks()
        {
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }));

            mockUserManager.Setup(x => x.GetUserAsync(user)).ReturnsAsync(GetUser());
            var piggyBankService = new Mock<IPiggyBankService>();
            var userMock = new Mock<DbSet<User>>();
            var piggyBankMock = new Mock<DbSet<PiggyBank>>();
            var walletMock = new Mock<DbSet<Wallet>>();
            var dbMock = new Mock<BankContext>();

            return (mockUserManager, piggyBankService, userMock, piggyBankMock, walletMock, dbMock, user);
        }

        private static void SetSettingsInDb<T>(Mock<DbSet<T>> mock, Mock<IQueryable<T>> items) where T: class
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
                PhoneNumber = "380111111111",
                PiggyBanks = GetPiggyBanks()
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
                    PiggyBanks = GetPiggyBanks()

                },
                new User()
                {
                    Id = "2",
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "380111111111",
                    PiggyBanks = GetPiggyBanks()
                },
                new User()
                {
                    Id = "3",
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "380111111111",
                    PiggyBanks = GetPiggyBanks()
                },
                new User()
                {
                    Id = "4",
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "380111111111",
                    PiggyBanks = GetPiggyBanks()
                }
            };
        }

        private static List<PiggyBank> GetPiggyBanks()
        {
            return new List<PiggyBank>()
            {
                new PiggyBank()
                {
                    Currency = Currencies.EUR,
                    Id = 1,
                    Money = 100,
                    User = new User()
                    {
                        Id = "1",
                        FirstName = "test",
                        LastName = "test",
                        Email = "test@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "380111111111"
                    }
                },
                new PiggyBank()
                {
                    Currency = Currencies.RUB,
                    Id = 2,
                    Money = 100,
                    User = new User()
                    {
                        Id = "1",
                        FirstName = "test",
                        LastName = "test",
                        Email = "test@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "380111111111"
                    }
                },
                new PiggyBank()
                {
                    Currency = Currencies.UAH,
                    Id = 3,
                    Money = 100,
                    User = new User()
                    {
                        Id = "1",
                        FirstName = "test",
                        LastName = "test",
                        Email = "test@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "380111111111"
                    }
                },
                new PiggyBank()
                {
                    Currency = Currencies.USD,
                    Id = 4,
                    Money = 100,
                    User = new User()
                    {
                        Id = "1",
                        FirstName = "test",
                        LastName = "test",
                        Email = "test@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "380111111111"
                    }
                }
            };
        }

        private static List<Wallet> GetWallets()
        {
            return new List<Wallet>()
            {
                new Wallet()
                {
                    Id = 1,
                    Code = "1005",
                    Currency = Currencies.EUR,
                    Money = 100,
                    Number = "1111111111111111",
                    User = new User()
                    {
                        Id = "1",
                        FirstName = "test",
                        LastName = "test",
                        Email = "test@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "380111111111"
                    }
                },
                new Wallet()
                {
                    Id = 2,
                    Code = "1005",
                    Currency = Currencies.RUB,
                    Money = 100,
                    Number = "2222222222222222",
                    User = new User()
                    {
                        Id = "1",
                        FirstName = "test",
                        LastName = "test",
                        Email = "test@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "380111111111"
                    }
                },
                new Wallet()
                {
                    Id = 3,
                    Code = "1005",
                    Currency = Currencies.UAH,
                    Money = 100,
                    Number = "3333333333333333",
                    User = new User()
                    {
                        Id = "1",
                        FirstName = "test",
                        LastName = "test",
                        Email = "test@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "380111111111"
                    }
                },
                new Wallet()
                {
                    Id = 4,
                    Code = "1005",
                    Currency = Currencies.USD,
                    Money = 100,
                    Number = "4444444444444444",
                    User = new User()
                    {
                        Id = "1",
                        FirstName = "test",
                        LastName = "test",
                        Email = "test@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "380111111111"
                    }
                }
            };
        }
    }
}
