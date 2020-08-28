using BankApp.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BankApp.WEB.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager">IUserService</param>
        public HomeController(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork), " was null.");
            }

            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Main home page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            var user = unitOfWork.UserManager.GetUserAsync(User).Result;
            return View("Index", user);
        }
    }
}
