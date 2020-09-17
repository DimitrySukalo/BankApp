using BankApp.BLL.Interfaces;
using BankApp.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BankApp.WEB.Controllers
{
    /// <summary>
    /// Profile controller
    /// </summary>
    [Authorize]
    public class ProfileController : Controller
    {
        /// <summary>
        /// Wallet service
        /// </summary>
        private readonly IWalletService walletService;

        /// <summary>
        /// Initialization
        /// </summary>
        public ProfileController(IWalletService walletService)
        {
            if(walletService == null)
            {
                throw new ArgumentNullException(nameof(walletService), " was null.");
            }

            this.walletService = walletService;
        }

        /// <summary>
        /// Main profile page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Getting current user from the session
            var user = await walletService.UnitOfWork.UserManager.GetUserAsync(User);
            
            //Getting all user wallets from the database
            var wallets = await walletService.UnitOfWork.WalletRepository.GetAllUserWalletsAync(user);

            //Making wallet view model
            var walletViewModel = new WalletViewModel()
            {
                Wallets = wallets
            };

            //Returning profile page for user
            return View("Profile", walletViewModel);
        }
    }
}
