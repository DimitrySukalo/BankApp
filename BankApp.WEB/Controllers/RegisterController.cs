using AutoMapper;
using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BankApp.WEB.Controllers
{
    public class RegisterController : Controller
    {
        //Register service
        private readonly IRegisterService registerService;

        /// <summary>
        /// Initialization
        /// </summary>
        public RegisterController(IRegisterService registerService)
        {
            if(registerService == null)
            {
                throw new ArgumentNullException(nameof(registerService), " was null.");
            }

            this.registerService = registerService;
        }

        /// <summary>
        /// Register form
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            return View("Register");
        }

        /// <summary>
        /// User registration
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterAccount(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                //Mapper configuration
                var configuration = new MapperConfiguration(cnf => cnf.CreateMap<RegisterViewModel, UserDTO>());

                //Creating mapper
                var mapper = new Mapper(configuration);

                if (registerViewModel != null)
                {
                    //Creating userDTO
                    var userDTO = mapper.Map<RegisterViewModel, UserDTO>(registerViewModel);

                    if (userDTO != null)
                    {
                        var result = registerService.RegisterUserAsync(userDTO);
                        if(result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            //Error messages
                            foreach(var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }

                            return View("Register", registerViewModel);
                        }
                    }
                    else
                    {
                        return View("Register", registerViewModel);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User dto is null.");
                    return View("Register", registerViewModel);
                }
            }
            else
            {
                return View("Register", registerViewModel);
            }
        }
    }
}
