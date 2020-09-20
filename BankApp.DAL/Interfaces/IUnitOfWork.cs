using BankApp.DAL.Entities;
using BankApp.DAL.Entity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BankApp.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Sign in manager
        /// </summary>
        SignInManager<User> SignInManager { get; }

        /// <summary>
        /// Database
        /// </summary>
        BankContext Database { get; }

        /// <summary>
        /// User manager
        /// </summary>
        UserManager<User> UserManager { get; set; }

        /// <summary>
        /// Wallet repository
        /// </summary>
        IWalletRepository WalletRepository { get; }

        /// <summary>
        /// Country repository
        /// </summary>
        ICountryRepository CountryRepository { get; }

        /// <summary>
        /// Save data async
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
