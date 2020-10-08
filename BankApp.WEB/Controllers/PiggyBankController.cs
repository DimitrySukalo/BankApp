using AutoMapper;
using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.DAL.Entity;
using BankApp.DAL.Interfaces;
using BankApp.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BankApp.WEB.Controllers
{
    /// <summary>
    /// Piggy bank controller
    /// </summary>
    [Authorize]
    public class PiggyBankController : Controller
    {
        /// <summary>
        /// User manager
        /// </summary>
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Piggy bank service
        /// </summary>
        private readonly IPiggyBankService piggyBankService;

        /// <summary>
        /// Database
        /// </summary>
        private readonly BankContext db;

        /// <summary>
        /// Unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initialization
        /// </summary>
        public PiggyBankController(UserManager<User> userManager, BankContext context, IPiggyBankService piggyBankService, IUnitOfWork unitOfWork)
        {
            if(userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager), " was null.");
            }

            if(context == null)
            {
                throw new ArgumentNullException(nameof(context), " was null.");
            }

            if(piggyBankService == null)
            {
                throw new ArgumentNullException(nameof(piggyBankService), "was null.");
            }

            if(unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork), "was null.");
            }

            this.userManager = userManager;
            db = context;
            this.piggyBankService = piggyBankService;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Main page of piggy bank
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Getting user from the session
            var user = await userManager.GetUserAsync(User);

            //Getting user from database
            var userDB = await db.Users.Include(u => u.PiggyBanks).FirstOrDefaultAsync(u => u.Id == user.Id);

            var piggyBankViewModel = new PiggyBankViewModel()
            {
                User = userDB,
                WithdrawSum = 0.0m
            };

            return View(piggyBankViewModel);
        }

        /// <summary>
        /// Withdrawing money from the piggy bank
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Withdraw(PiggyBankViewModel piggyBankViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    var userInDb = await db.Users.Include(u => u.PiggyBanks).Include(u => u.Wallets).FirstOrDefaultAsync(u => u.Id == currentUser.Id);

                    piggyBankViewModel.User = userInDb;

                    //Mapper configuration
                    var configuration = new MapperConfiguration(cnf => cnf.CreateMap<PiggyBankViewModel, PiggyBankDTO>());

                    //Creating mapper
                    var mapper = new Mapper(configuration);

                    //Creating piggy bank DTO
                    var piggyBankDTO = mapper.Map<PiggyBankViewModel, PiggyBankDTO>(piggyBankViewModel);

                    var piggyBank = await unitOfWork.PiggyBankRepository.GetByIdAsync(piggyBankViewModel.PiggyBankId);
                    piggyBankDTO.PiggyBank = piggyBank;

                    var result = await piggyBankService.WithdrawMoneyFromPiggyBankAsync(piggyBankDTO);
                    if (result.Successed)
                    {
                        return RedirectToAction("Index", "Profile");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Message);

                        piggyBankViewModel.User = userInDb;
                        return View("Index", piggyBankViewModel);
                    }
                }
                else
                {
                    return Content("Error 404");
                }
            }
            else
            {
                var currentUser = await userManager.GetUserAsync(User);
                var userInDb = await db.Users.Include(u => u.PiggyBanks).FirstOrDefaultAsync(u => u.Id == currentUser.Id);

                piggyBankViewModel.User = userInDb;
                return View("Index", piggyBankViewModel);
            }
        }
    }
}
