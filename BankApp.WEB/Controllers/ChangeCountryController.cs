using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.DAL.Entity;
using BankApp.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankApp.WEB.Controllers
{
    /// <summary>
    /// Change country controller
    /// </summary>
    [Authorize]
    public class ChangeCountryController : Controller
    {
        //Setting service
        private readonly ISettingService settingService;

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
        public ChangeCountryController(ISettingService settingService, UserManager<User> userManager, BankContext context)
        {
            if(settingService == null)
            {
                throw new ArgumentNullException(nameof(settingService), " was null.");
            }
            
            if(userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager), " was null.");
            }

            if(context == null)
            {
                throw new ArgumentNullException(nameof(context), " was null.");
            }

            this.settingService = settingService;
            this.userManager = userManager;
            db = context;
        }

        /// <summary>
        /// Change country page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ChangeCountry()
        {
            //Getting current user in the session
            var user = await userManager.GetUserAsync(User);

            //Getting user from the database with the country
            var userDB = await db.Users.Include(u => u.Country).FirstOrDefaultAsync(u => u.Id == user.Id);

            var changeDataViewModel = new ChangeDataViewModel()
            {
                Country = userDB.Country
            };

            return View("ChangeCountry", changeDataViewModel);
        }

        /// <summary>
        /// Change country method
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeCountry(ChangeDataViewModel changeDataViewModel)
        {
            //Checking if data is correct
            if (changeDataViewModel.Country == null ||
                string.IsNullOrWhiteSpace(changeDataViewModel.Country.CountryName) ||
                string.IsNullOrWhiteSpace(changeDataViewModel.Country.City) ||
                !Regex.IsMatch(changeDataViewModel.Country.CountryName, @"^[A-z]") ||
                !Regex.IsMatch(changeDataViewModel.Country.City, @"^[A-z]"))
            {
                ModelState.AddModelError(string.Empty, "Data is not correct");
                return View("ChangeCountry", changeDataViewModel);
            }
            else
            {
                //Getting current user in the session
                var user = await userManager.GetUserAsync(User);

                //Getting user from the database with the country
                var userDB = await db.Users.Include(u => u.Country).FirstOrDefaultAsync(u => u.Id == user.Id);

                var countryDTO = new CountryDTO()
                {
                    City = changeDataViewModel.Country.City,
                    CountryName = changeDataViewModel.Country.CountryName,
                    User = userDB
                };

                var result = await settingService.ChangeCountryAsync(countryDTO);
                if(result.Successed)
                {
                    return RedirectToAction("Index", "Setting");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View("ChangeCountry", changeDataViewModel);
                }
            }
        }
    }
}
