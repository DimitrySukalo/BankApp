using BankApp.DAL.Entity;
using BankApp.DAL.Interfaces;
using BankApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.DAL.Repositories
{
    /// <summary>
    /// Implementation of the country repository
    /// </summary>
    public class CountryRepository : ICountryRepository
    {
        /// <summary>
        /// Database
        /// </summary>
        private readonly BankContext db;

        /// <summary>
        /// Initialization
        /// </summary>
        public CountryRepository(BankContext context)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context), " was null.");
            }

            db = context;
        }

        /// <summary>
        /// Remove country from the database by id
        /// </summary>
        public async Task RemoveByIdCountryAsync(int id)
        {
            //Getting country from the database
            var country = await db.Countries.FirstOrDefaultAsync(c => c.Id == id);
            if(country != null)
            {
                //Removing country
                db.Countries.Remove(country);

                //Saving database
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Remove country from the database
        /// </summary>
        public async Task RemoveCountryAsync(Country country)
        {
            if(country != null && db.Countries.Contains(country))
            {
                //Removing country
                db.Remove(country);

                //Saving database
                await db.SaveChangesAsync();
            }
        }
    }
}
