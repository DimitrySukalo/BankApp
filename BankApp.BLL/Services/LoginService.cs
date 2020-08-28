using AutoMapper;
using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.DAL.Interfaces;
using BankApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace BankApp.BLL.Services
{
    /// <summary>
    /// Implementation of login service
    /// </summary>
    public class LoginService : ILoginService
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<LoginService> logger;

        /// <summary>
        /// Unit of work
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Initialization
        /// </summary>
        public LoginService(IUnitOfWork unitOfWork, ILogger<LoginService> logger)
        {
            if(unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork), " was null.");
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger), " was null.");
            }

            UnitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Sign in method
        /// </summary>
        public OperationSuccessed SignInUserAccount(UserLoginDTO userLoginDTO)
        {
            if(userLoginDTO != null)
            {
                //Checking if user is registered
                var user = UnitOfWork.UserManager.FindByEmailAsync(userLoginDTO.Email).Result;

                if (user != null)
                {
                    if (user.EmailConfirmed == true)
                    {
                        //Sign in user account
                        var result = UnitOfWork.SignInManager.PasswordSignInAsync(user.Email, user.Password, true, false).Result;
                        if (result.Succeeded)
                        {

                            //Console information about log in
                            logger.LogInformation("User logged in.");
                            return new OperationSuccessed("User have logged in", true);
                        }
                        else
                        {
                            return new OperationSuccessed("User is not registreted.", false);
                        }
                    }
                    else
                    {
                        logger.LogInformation("Email is not confirmed");
                        return new OperationSuccessed("Email is not confrmed", false);
                    }
                }
                else
                {
                    logger.LogInformation("User entity is null.");
                    return new OperationSuccessed("User doen't exist.", false);
                }
            }
            else
            {
                logger.LogInformation("User login dto is null.");
                return new OperationSuccessed("Error user data.", false);
            }
        }
    }
}
