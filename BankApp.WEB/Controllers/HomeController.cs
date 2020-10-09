using AutoMapper;
using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BankApp.WEB.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Home service
        /// </summary>
        private readonly IHomeService homeService;

        /// <summary>
        /// User manager
        /// </summary>
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager">IUserService</param>
        public HomeController(IHomeService homeService, UserManager<User> userManager)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager), " was null.");
            }

            if(homeService == null)
            {
                throw new ArgumentNullException(nameof(homeService), " was null.");
            }

            this.homeService = homeService;
            this.userManager = userManager;
        }

        /// <summary>
        /// Main home page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            var models = new MultiplyHomeModels()
            {
                User = user,
                MessageViewModel = new UserMessageViewModel()
            };

            return View("Index", models);
        }

        /// <summary>
        /// Send user message
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SendMessage(MultiplyHomeModels multiplyHomeModels)
        {
            if(ModelState.IsValid && multiplyHomeModels.MessageViewModel != null)
            {
                var userMessageViewModel = multiplyHomeModels.MessageViewModel;
                //Mapper configuration
                var configuration = new MapperConfiguration(conf => conf.CreateMap<UserMessageViewModel, UserMessageDTO>());

                //Mapper
                var mapper = new Mapper(configuration);

                //Creating user message
                var userMessageDTO = mapper.Map<UserMessageViewModel, UserMessageDTO>(userMessageViewModel);

                var result = await homeService.SaveUserMessageInDbAsync(userMessageDTO);
                if(result != null)
                {
                    if (result.Successed)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Content("Bad response");
                    }
                }
                else
                {
                    return Content("Bad response");
                }
            }
            else
            {
                //Finding user that to detect form of the home page
                var user = await userManager.GetUserAsync(User);
                multiplyHomeModels.User = user;

                //Error view model
                return View("Index", multiplyHomeModels);
            }
        }
    }
}
