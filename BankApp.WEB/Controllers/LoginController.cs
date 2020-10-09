using AutoMapper;
using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BankApp.WEB.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// Login service
        /// </summary>
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            if(loginService == null)
            {
                throw new ArgumentNullException();
            }

            this.loginService = loginService;
        }

        /// <summary>
        /// Login form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            //Sign out user account
            loginService.UnitOfWork.SignInManager.SignOutAsync();
            return View("Login");
        }

        /// <summary>
        /// User login
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult LoginAccount(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                //Mapper configuration
                var configuration = new MapperConfiguration(cnf => cnf.CreateMap<LoginViewModel, UserLoginDTO>());

                //Creating mapper
                var mapper = new Mapper(configuration);

                //Creating user
                var userDTO = mapper.Map<LoginViewModel, UserLoginDTO>(loginViewModel);

                if(userDTO != null)
                {
                    //Result if log in
                    var result = loginService.SignInUserAccount(userDTO);
                    if(result.Successed)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Message);
                        return View("Login", loginViewModel);
                    }
                }
                else
                {
                    return Content("Bad response");
                }
            }
            else
            {
                return View("Login", loginViewModel);
            }
        }
    }
}
