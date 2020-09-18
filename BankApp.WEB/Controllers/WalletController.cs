﻿using BankApp.BLL.Interfaces;
using BankApp.PL.ViewModels;
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
        /// Initialization
        /// </summary>
        public WalletController(IWalletService walletService)
        {
            if (walletService == null)
            {
                throw new ArgumentNullException(nameof(walletService), " was null.");
            }

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
            var user = await walletService.UnitOfWork.UserManager.GetUserAsync(User);

            //Getting user from the database
            var userDB = await walletService.UnitOfWork.Database.Users.Include(u => u.Country).FirstOrDefaultAsync(u => u.Id == user.Id);

            //Making wallet view model
            var walletViewModel = new WalletViewModel()
            {
                User = userDB
            };

            return View(walletViewModel);
        }
    }
}