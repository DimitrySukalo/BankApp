using BankApp.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp.DAL.Interfaces
{
    /// <summary>
    /// Wallet repository
    /// </summary>
    public interface IWalletRepository
    {
        /// <summary>
        /// Get all wallet from database
        /// </summary>
        /// <returns></returns>
        Task<List<Wallet>> GetAllWalletsAsync();

        /// <summary>
        /// Get wallet by id
        /// </summary>
        Task<Wallet> GetWalletByIdAsync(int id);

        /// <summary>
        /// Get all user wallets
        /// </summary>
        Task<List<Wallet>> GetAllUserWalletsAync(User user);

        /// <summary>
        /// Add wallet to the database
        /// </summary>
        Task AddWalletAsync(Wallet wallet);

        /// <summary>
        /// Remove wallet from the database
        /// </summary>
        Task RemoveWalletAsync(Wallet wallet);

        /// <summary>
        /// Remove wallet from the database by id 
        /// </summary>
        Task RemoveWalletByIdAsync(int id);
    }
}
