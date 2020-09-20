using AutoMapper;
using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BankApp.WEB.Controllers
{
    public class RegisterController : Controller
    {
        /// <summary>
        /// Register service
        /// </summary>
        private readonly IRegisterService registerService;

        /// <summary>
        /// Email service
        /// </summary>
        private readonly ISendEmailService sendEmailService;

        /// <summary>
        /// Initialization
        /// </summary>
        public RegisterController(IRegisterService registerService, ISendEmailService emailService)
        {
            if(registerService == null)
            {
                throw new ArgumentNullException(nameof(registerService), " was null.");
            }

            if(emailService == null)
            {
                throw new ArgumentNullException(nameof(emailService), " was null.");
            }

            this.registerService = registerService;
            sendEmailService = emailService;
        }

        /// <summary>
        /// Register form
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            //Sign out user account
            registerService.UnitOfWork.SignInManager.SignOutAsync();
            return View("Register");
        }

        /// <summary>
        /// User registration
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterAccount(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                //Mapper configuration
                var configuration = new MapperConfiguration(cnf => cnf.CreateMap<RegisterViewModel, UserRegisterDTO>());

                //Creating mapper
                var mapper = new Mapper(configuration);

                if (registerViewModel != null)
                {
                    //Creating userDTO
                    var userDTO = mapper.Map<RegisterViewModel, UserRegisterDTO>(registerViewModel);

                    if (userDTO != null || CheckEmailValidation(userDTO.Email))
                    {
                        var result = registerService.RegisterUser(userDTO);
                        if(result.Succeeded)
                        {
                            //Actual user who want to register
                            var user = registerService.UnitOfWork.Database.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email).Result;

                            //Confirmation code
                            var code = registerService.UnitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(user).Result;

                            var callBackUrl = Url.Action(
                                "ConfirmEmail",
                                "Register",
                                new { userId = user.Id, code },
                                protocol: HttpContext.Request.Scheme
                                );

                            //Sending email message
                            sendEmailService.SendEmail(user.Email, "Confirm your account", $"Confirm registration by clicking on the link: {callBackUrl}");

                            return View("ConfirmMessage");
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

        /// <summary>
        /// Checking if email address is valid
        /// </summary>
        private bool CheckEmailValidation(string emailAddress)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(emailAddress);

                return true;
            }
            catch(FormatException)
            {
                ModelState.AddModelError(string.Empty, "Email is not correct");
                return false;
            }
        }

        /// <summary>
        /// Email confirmation
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ConfirmEmail(string userId, string code)
        {
            if(string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                return Content("Error");
            }
            else
            {
                //Current user
                var user = registerService.UnitOfWork.UserManager.FindByIdAsync(userId).Result;
                if(user != null)
                {
                    //Result of email confirmation
                    var result = registerService.UnitOfWork.UserManager.ConfirmEmailAsync(user, code).Result;
                    if(result.Succeeded)
                    {
                        //Successed confirmation
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        //Email is not confirmed
                        return Content("Email is not confirmed");
                    }
                }
                else
                {
                    return Content("This user does not exist in the database");
                }
            }
        }
    }
}
