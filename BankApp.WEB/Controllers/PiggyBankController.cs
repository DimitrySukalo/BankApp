using BankApp.DAL.Entities;
using BankApp.DAL.Entity;
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
        /// Database
        /// </summary>
        private readonly BankContext db;

        /// <summary>
        /// Initialization
        /// </summary>
        public PiggyBankController(UserManager<User> userManager, BankContext context)
        {
            if(userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager), " was null.");
            }

            if(context == null)
            {
                throw new ArgumentNullException(nameof(context), " was null.");
            }

            this.userManager = userManager;
            db = context;
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
            var userDB = await db.Users.Include(u => u.PiggyBank).FirstOrDefaultAsync(u => u.Id == user.Id);

            var piggyBankViewModel = new PiggyBankViewModel()
            {
                User = userDB,
                WithdrawSum = 0.0m
            };

            return View(piggyBankViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(PiggyBankViewModel piggyBankViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    var userInDb = await db.Users.Include(u => u.PiggyBank).FirstOrDefaultAsync(u => u.Id == currentUser.Id);
                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    return Content("Error 404");
                }
            }
            else
            {
                var currentUser = await userManager.GetUserAsync(User);
                var userInDb = await db.Users.Include(u => u.PiggyBank).FirstOrDefaultAsync(u => u.Id == currentUser.Id);

                piggyBankViewModel.User = userInDb;
                return View(piggyBankViewModel);
            }
        }
    }
}
