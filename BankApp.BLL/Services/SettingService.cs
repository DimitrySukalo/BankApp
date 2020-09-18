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
        public async Task ChangeCountryAsync(CountryDTO countryDTO)
        {
            if(countryDTO != null)
            {
                //Getting user from the database
                var user = await unitOfWork.Database.Users.Include(u => u.Country).FirstOrDefaultAsync(u => u.Id == countryDTO.User.Id);
                if(user != null)
                {
                    //Creating a country
                    var country = new Country()
                    {
                        City = countryDTO.City,
                        CountryName = countryDTO.CountryName
                    };

                    user.Country = country;

                    //Saving database
                    await unitOfWork.SaveAsync();
                }
            }
        }
    }
}
