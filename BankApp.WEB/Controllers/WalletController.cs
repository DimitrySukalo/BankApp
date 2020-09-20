using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
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
        /// Initialization
        /// </summary>
        public WalletController(IWalletService walletService, UserManager<User> userManager)
        {
            if (walletService == null)
            {
                throw new ArgumentNullException(nameof(walletService), " was null.");
            }

            if(userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager), " was null.");
            }

            this.userManager = userManager;
            this.walletService = walletService;
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
    }
}
