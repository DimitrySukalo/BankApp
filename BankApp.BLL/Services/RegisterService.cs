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
        /// Unit of work
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Initialization
        /// </summary>
        public RegisterService(IUnitOfWork unitOfWork)
        {
            if(unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork), " was null.");
            }
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Register user
        /// </summary>
        public IdentityResult RegisterUser(UserRegisterDTO userDTO)
        {
            if(userDTO != null)
            {
                //Mapper configuration
                var configuration = new MapperConfiguration(cnf => cnf.CreateMap<UserRegisterDTO, User>()
                                    .ForMember("UserName", opt => opt.MapFrom(dto => dto.Email)));

                //Creating mapper
                var mapper = new Mapper(configuration);

                //Creating user
                var user = mapper.Map<UserRegisterDTO, User>(userDTO);

                if(user != null)
                { 
                    user.PiggyBank = new PiggyBank()
                    {
                        User = user,
                        Money = 0.0m
                    };

                    //Result of user registration
                    var result = UnitOfWork.UserManager.CreateAsync(user, user.Password).Result;

                    if (result.Succeeded)
                    {
                        //Saving changes in the database
                        UnitOfWork.SaveAsync();

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
