using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Interfaces;
using BankApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BankApp.BLL.Services
{
    /// <summary>
    /// Implementation of the setting serivce
    /// </summary>
    public class SettingService : ISettingService
    {
        /// <summary>
        /// unit of work
        /// </summary>
        public IUnitOfWork unitOfWork { get; }

        /// <summary>
        /// Initialization
        /// </summary>
        public SettingService(IUnitOfWork unitOfWork)
        {
            if(unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork), " was null.");
            }

            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Setting service
        /// </summary>
        public async Task<OperationSuccessed> ChangeCountryAsync(CountryDTO countryDTO)
        {
            if(countryDTO != null)
            {
                //Getting user from the database
                var user = await unitOfWork.Database.Users.Include(u => u.Country).FirstOrDefaultAsync(u => u.Id == countryDTO.User.Id);
                if(user != null)
                {
                    //Saving user country that to delete from the database
                    var oldCountry = user.Country;

                    //Removing country from the database
                    await unitOfWork.CountryRepository.RemoveCountryAsync(oldCountry);

                    //Creating a country
                    var country = new Country()
                    {
                        City = countryDTO.City,
                        CountryName = countryDTO.CountryName
                    };

                    user.Country = country;

                    //Saving database
                    await unitOfWork.SaveAsync();

                    return new OperationSuccessed("Data changed", true);
                }
                else
                {
                    return new OperationSuccessed("Data is not correct", false);
                }
            }
            else
            {
                return new OperationSuccessed("Data is not correct", false);
            }
        }

        /// <summary>
        /// Change number method
        /// </summary>
        public async Task<OperationSuccessed> ChangeNumberAsync(ChangeNumberDTO changeNumberDTO)
        {
            //Getting user from the databse
            var userDB = await unitOfWork.Database.Users.FirstOrDefaultAsync(u => u.Id == changeNumberDTO.User.Id);

            if(changeNumberDTO != null && !string.IsNullOrWhiteSpace(changeNumberDTO.Number) && userDB != null)
            {
                //Result of setting phone number
                var result = await unitOfWork.UserManager.SetPhoneNumberAsync(userDB, changeNumberDTO.Number);
                if(result.Succeeded)
                {
                    //Saving database
                    await unitOfWork.Database.SaveChangesAsync();

                    return new OperationSuccessed("Data is changed", true);
                }
                else
                {
                    return new OperationSuccessed("Data is not correct.Template of the phone number: 380XXXXXXXXX", false);
                }
            }
            else
            {
                return new OperationSuccessed("Data is not correct.Template of the phone number: 380XXXXXXXXX", false);
            }
        }
    }
}
