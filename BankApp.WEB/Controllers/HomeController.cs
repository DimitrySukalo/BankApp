using AutoMapper;
using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Interfaces;
using BankApp.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BankApp.WEB.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Home service
        /// </summary>
        private readonly IHomeService homeService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager">IUserService</param>
        public HomeController(IUnitOfWork unitOfWork, IHomeService homeService)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork), " was null.");
            }

            if(homeService == null)
            {
                throw new ArgumentNullException(nameof(homeService), " was null.");
            }

            this.homeService = homeService;
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
            if(ModelState.IsValid)
            {
                var userMessageViewModel = multiplyHomeModels.MessageViewModel;
                if(userMessageViewModel != null)
                {
                    //Mapper configuration
                    var configuration = new MapperConfiguration(conf => conf.CreateMap<UserMessageViewModel, UserMessageDTO>());

                    //Mapper
                    var mapper = new Mapper(configuration);

                    //Creating user message
                    var userMessageDTO = mapper.Map<UserMessageViewModel, UserMessageDTO>(userMessageViewModel);

                    var result = await homeService.SaveUserMessageInDbAsync(userMessageDTO);
                    if(result.Successed)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Content("Bad request");
                    }
                }
                else
                {
                    return Content("Bad request");
                }
            }
            else
            {
                //Finding user that to detect form of the home page
                var user = await unitOfWork.UserManager.GetUserAsync(User);
                multiplyHomeModels.User = user;

                //Error view model
                return View("Index", multiplyHomeModels); //Не показывает ошибки при вводе неправильной информации
            }
        }
    }
}
