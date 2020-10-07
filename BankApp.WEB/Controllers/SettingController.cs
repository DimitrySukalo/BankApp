using BankApp.BLL.Interfaces;
using BankApp.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BankApp.WEB.Controllers
{
    /// <summary>
    /// Profile setting controller
    /// </summary>
    [Authorize]
    public class SettingController : Controller
    {
        /// <summary>
        /// Wallet service
        /// </summary>
        private readonly IWalletService walletService;

        /// <summary>
        /// Initialization
        /// </summary>
        public SettingController(IWalletService walletService)
        {
            if (walletService == null)
            {
                throw new ArgumentNullException(nameof(walletService), " was null.");
            }

            this.walletService = walletService;
        }

        /// <summary>
        /// Setting profile page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Getting current user from the session
            var user = await walletService.UnitOfWork.UserManager.GetUserAsync(User);

            //Getting user from the database
            var userDB = await walletService.UnitOfWork.Database.Users.Include(u => u.Country).FirstOrDefaultAsync(u => u.Id == user.Id);

            var wallets = await walletService.UnitOfWork.WalletRepository.GetAllUserWalletsAync(userDB);

            var profileModel = new ProfileViewModel()
            {
                User = userDB,
                Wallets = wallets
            };

            return View(profileModel);
        }

    }
}
