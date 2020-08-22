using AutoMapper;
using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace BankApp.BLL.Services
{
    /// <summary>
    /// Implementation of register service
    /// </summary>
    public class RegisterService : IRegisterService
    {
        /// <summary>
        /// IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initialization
        /// </summary>
        public RegisterService(IUnitOfWork unitOfWork)
        {
            if(unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork), " was null.");
            }

            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Register user
        /// </summary>
        public IdentityResult RegisterUserAsync(UserDTO userDTO)
        {
            if(userDTO != null)
            {
                //Mapper configuration
                var configuration = new MapperConfiguration(cnf => cnf.CreateMap<UserDTO, User>()
                                    .ForMember("UserName", opt => opt.MapFrom(dto => dto.Email)));

                //Creating mapper
                var mapper = new Mapper(configuration);

                //Creating user
                var user = mapper.Map<UserDTO, User>(userDTO);

                if(user != null)
                {
                    //Result of user registration
                    var result = unitOfWork.UserManager.CreateAsync(user, user.Password).Result;

                    if (result.Succeeded)
                    {
                        //Saving changes in the database
                         unitOfWork.SaveAsync();

                        return result;
                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
