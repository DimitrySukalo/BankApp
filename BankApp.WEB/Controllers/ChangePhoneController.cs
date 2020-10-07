using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankApp.WEB.Controllers
{
    /// <summary>
    /// Change phone controller
    /// </summary>
    [Authorize]
    public class ChangePhoneController : Controller
    {
        /// <summary>
        /// Setting service
        /// </summary>
        private readonly ISettingService settingService;

        /// <summary>
        /// User Manager
        /// </summary>
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Initialization
        /// </summary>
        public ChangePhoneController(ISettingService settingService, UserManager<User> userManager)
        {
            if (settingService == null)
            {
                throw new ArgumentNullException(nameof(settingService), " was null.");
            }

            if(userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager), " was null.");
            }

            this.userManager = userManager;
            this.settingService = settingService;
        }

        /// <summary>
        /// Change number page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ChangePhone()
        {
            //Getting current user in the session
            var user = await userManager.GetUserAsync(User);

            var changeDataViewModel = new ChangeDataViewModel()
            {
                PhoneNumber = user.PhoneNumber
            };

            return View("ChangePhone", changeDataViewModel);
        }

        /// <summary>
        /// Change phone number method
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePhone(ChangeDataViewModel changeDataViewModel)
        {
            if(string.IsNullOrWhiteSpace(changeDataViewModel.PhoneNumber) || changeDataViewModel == null ||
                !Regex.IsMatch(changeDataViewModel.PhoneNumber, @"380[0-9]{9}"))
            {
                ModelState.AddModelError(string.Empty, "Data is not correct. Template of the phone number: 380XXXXXXXXX");
                return View("ChangePhone", changeDataViewModel);
            }
            else
            {
                var changeNumberDTO = new ChangeNumberDTO()
                {
                    Number = changeDataViewModel.PhoneNumber,
                    User = await settingService.unitOfWork.UserManager.GetUserAsync(User)
                };

                //Changing user phine number
                var result = await settingService.ChangeNumberAsync(changeNumberDTO);
                if(result.Successed)
                {
                    return RedirectToAction("Index", "Setting");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View("ChangePhone", changeNumberDTO);
                }
            }
        }
    }
}
