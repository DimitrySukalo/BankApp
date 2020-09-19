using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.PL.ViewModels;
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
    public class ChangeCountryController : Controller
    {
        //Setting service
        private readonly ISettingService settingService;

        /// <summary>
        /// Initialization
        /// </summary>
        public ChangeCountryController(ISettingService settingService)
        {
            if(settingService == null)
            {
                throw new ArgumentNullException(nameof(settingService), " was null.");
            }

            this.settingService = settingService;
        }

        /// <summary>
        /// Change country page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ChangeCountry()
        {
            //Getting current user in the session
            var user = await settingService.unitOfWork.UserManager.GetUserAsync(User);

            //Getting user from the database with the country
            var userDB = await settingService.unitOfWork.Database.Users.Include(u => u.Country).FirstOrDefaultAsync(u => u.Id == user.Id);

            var changeDataViewModel = new ChangeDataViewModel()
            {
                Country = userDB.Country
            };

            return View(changeDataViewModel);
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
                return View(changeDataViewModel);
            }
            else
            {
                //Getting current user in the session
                var user = await settingService.unitOfWork.UserManager.GetUserAsync(User);

                //Getting user from the database with the country
                var userDB = await settingService.unitOfWork.Database.Users.Include(u => u.Country).FirstOrDefaultAsync(u => u.Id == user.Id);

                var countryDTO = new CountryDTO()
                {
                    City = changeDataViewModel.Country.City,
                    CountryName = changeDataViewModel.Country.CountryName,
                    User = userDB
                };

                await settingService.ChangeCountryAsync(countryDTO);

                return RedirectToAction("Index", "Setting");
            }
        }
    }
}
