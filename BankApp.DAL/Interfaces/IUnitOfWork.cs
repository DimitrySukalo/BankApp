using BankApp.DAL.Entities;
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
        /// User manager
        /// </summary>
        UserManager<User> UserManager { get; }

        /// <summary>
        /// Save data async
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
