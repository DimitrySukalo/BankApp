using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BankApp.WEB.Controllers
{
    /// <summary>
    /// Change phone controller
    /// </summary>
    public class ChangePhoneController : Controller
    {
        //Setting service
        private readonly ISettingService settingService;

        /// <summary>
        /// Initialization
        /// </summary>
        public ChangePhoneController(ISettingService settingService)
        {
            if (settingService == null)
            {
                throw new ArgumentNullException(nameof(settingService), " was null.");
            }

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
            var user = await settingService.unitOfWork.UserManager.GetUserAsync(User);

            //Getting user from the database with the country
            var userDB = await settingService.unitOfWork.Database.Users.Include(u => u.Country).FirstOrDefaultAsync(u => u.Id == user.Id);

            var changeDataViewModel = new ChangeDataViewModel()
            {
                PhoneNumber = user.PhoneNumber
            };

            return View(changeDataViewModel);
        }

        /// <summary>
        /// Change phone number method
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePhone(ChangeDataViewModel changeDataViewModel)
        {
            if(string.IsNullOrWhiteSpace(changeDataViewModel.PhoneNumber) || changeDataViewModel == null)
            {
                ModelState.AddModelError(string.Empty, "Data is not correct");
                return View(changeDataViewModel);
            }
            else
            {
                var changeNumberDTO = new ChangeNumberDTO()
                {
                    Number = changeDataViewModel.PhoneNumber,
                    User = await settingService.unitOfWork.UserManager.GetUserAsync(User)
                };

                //Changing user phine number
                await settingService.ChangeNumberAsync(changeNumberDTO);
                return RedirectToAction("Index", "Setting");
            }
        }
    }
}
