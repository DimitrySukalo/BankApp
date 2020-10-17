using AutoMapper;
using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp.WEB.Controllers
{
    /// <summary>
    /// Transact controller
    /// </summary>
    [Authorize]
    public class TransactController : Controller
    {
        /// <summary>
        /// Transaction service
        /// </summary>
        private readonly ITransactService transactService;

        private readonly IWalletService walletService;

        private readonly UserManager<User> userManager;

        public TransactController(ITransactService transact, IWalletService walletService, UserManager<User> userManager)
        {
            if(transact == null)
            {
                throw new ArgumentNullException(nameof(transactService), " was null.");
            }

            if(userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager), " was null.");
            }

            if(walletService == null)
            {
                throw new ArgumentNullException(nameof(walletService), " was null.");
            }

            transactService = transact;
            this.walletService = walletService;
            this.userManager = userManager;
        }

        /// <summary>
        /// Page of transaction
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Wallet> wallets = await GetWallets();

            var transactionViewModel = new TransactionViewModel()
            {
                Wallets = wallets
            };

            return View("Index", transactionViewModel);
        }

        private async Task<List<Wallet>> GetWallets()
        {
            var user = await userManager.GetUserAsync(User);
            var wallets = await walletService.UnitOfWork.WalletRepository.GetAllUserWalletsAync(user);
            return wallets;
        }

        /// <summary>
        /// Make transaction method
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> MakeTransaction(TransactionViewModel transactionViewModel)
        {
            if(ModelState.IsValid)
            {
                var walletFrom = await walletService.UnitOfWork.WalletRepository.GetWalletByIdAsync(transactionViewModel.WalletFromId);
                var walletTo = await walletService.UnitOfWork.WalletRepository.GetByNumberAsync(transactionViewModel.WalletToNumber);

                //Mapper configuration
                var configuration = new MapperConfiguration(conf => conf.CreateMap<Wallet, WalletDTO>());

                //Mapper
                var mapper = new Mapper(configuration);

                //Creating user message
                var fromWallet = mapper.Map<Wallet, WalletDTO>(walletFrom);
                var toWallet = mapper.Map<Wallet, WalletDTO>(walletTo);

                var result = await transactService.TransactMoney(fromWallet, toWallet, transactionViewModel.SumOfTransaction);
                if(result.Successed)
                {
                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    transactionViewModel.Wallets = await GetWallets();
                    ModelState.AddModelError(string.Empty, result.Message);

                    return View("Index", transactionViewModel);
                }
            }
            else
            {
                transactionViewModel.Wallets = await GetWallets();
                return View("Index", transactionViewModel);
            }
        }
    }
}
