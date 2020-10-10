﻿using BankApp.BLL.Interfaces;
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
        /// History service
        /// </summary>
        private readonly IHistoryService historyService;

        /// <summary>
        /// Wallet service
        /// </summary>
        private readonly IWalletService walletService;

        /// <summary>
        /// Initialization
        /// </summary>
        public ProfileController(IWalletService walletService, IHistoryService historyService)
        {
            if(walletService == null)
            {
                throw new ArgumentNullException(nameof(walletService), " was null.");
            }

            if (historyService == null)
            {
                throw new ArgumentNullException(nameof(historyService), " was null.");
            }

            this.historyService = historyService;
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
            var histories = await historyService.GeAllHistories();

            //Making wallet view model
            var walletViewModel = new WalletViewModel()
            {
                Wallets = wallets,
                Histories = histories
            };

            //Returning profile page for user
            return View("Profile", walletViewModel);
        }
    }
}
