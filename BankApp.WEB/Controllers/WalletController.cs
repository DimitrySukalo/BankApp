using AutoMapper;
using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.DAL.Entity;
using BankApp.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BankApp.WEB.Controllers
{
    /// <summary>
    /// Wallet controller
    /// </summary>
    public class WalletController : Controller
    {
        /// <summary>
        /// Wallet service
        /// </summary>
        private readonly IWalletService walletService;

        /// <summary>
        /// User manager
        /// </summary>
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Database
        /// </summary>
        private readonly BankContext db;

        /// <summary>
        /// Initialization
        /// </summary>
        public WalletController(IWalletService walletService, UserManager<User> userManager, BankContext context)
        {
            if (walletService == null)
            {
                throw new ArgumentNullException(nameof(walletService), " was null.");
            }

            if(userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager), " was null.");
            }

            if(context == null)
            {
                throw new ArgumentNullException(nameof(context), " was null.");
            }

            this.userManager = userManager;
            this.walletService = walletService;
            db = context;
        }

        /// <summary>
        /// Add wallet controller
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AddWallet()
        {
            //Getting current user from the session
            var user = await userManager.GetUserAsync(User);

            //Making wallet view model
            var walletViewModel = new WalletViewModel()
            {
                User = user
            };

            return View("AddWallet", walletViewModel);
        }

        /// <summary>
        /// Adding wallet
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddWallet(WalletViewModel walletViewModel)
        {
            if(ModelState.IsValid)
            {
                //Getting current user from the session
                var user = await userManager.GetUserAsync(User);

                //Getting user from the database
                var userDB = await db.Users.Include(u => u.Country).FirstOrDefaultAsync(u => u.Id == user.Id);

                if (user.Country == null || string.IsNullOrWhiteSpace(user.Country.City) || string.IsNullOrWhiteSpace(user.Country.CountryName)
                    || string.IsNullOrWhiteSpace(user.PhoneNumber))
                {
                    ModelState.AddModelError(string.Empty, "Your profile data is not filled. Please check your data in \"Setting\" section");
                    walletViewModel.User = user;
                    return View("AddWallet", walletViewModel);
                }
                else
                {
                    walletViewModel.User = user;

                    //Mapper configuration
                    var configuration = new MapperConfiguration(conf => conf.CreateMap<WalletViewModel, WalletDTO>());

                    //Mapper
                    var mapper = new Mapper(configuration);

                    //Creating user message
                    var walletDTO = mapper.Map<WalletViewModel, WalletDTO>(walletViewModel);

                    var result = await walletService.AddWalletAsync(walletDTO);
                    if (result.Successed)
                    {
                        return RedirectToAction("Index", "Profile");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Wallet is not added");
                        walletViewModel.User = user;
                        return View("AddWallet", walletViewModel);
                    }
                }
            }
            else
            {
                //Getting current user from the session
                var user = await userManager.GetUserAsync(User);
                walletViewModel.User = user;

                return View("AddWallet", walletViewModel);
            }
        }
    }
}
