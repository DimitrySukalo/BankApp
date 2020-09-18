using BankApp.DAL.Models;
using System.Threading.Tasks;

namespace BankApp.DAL.Interfaces
{
    /// <summary>
    /// Country repository
    /// </summary>
    public interface ICountryRepository
    {
        /// <summary>
        /// Remove country from the database
        /// </summary>
        Task RemoveCountryAsync(Country country);

        /// <summary>
        /// Remove country from the database by id
        /// </summary>
        Task RemoveByIdCountryAsync(int id);
    }
}
