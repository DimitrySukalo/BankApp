using BankApp.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp.DAL.Interfaces
{
    /// <summary>
    /// History Repository
    /// </summary>
    public interface IHistoryRepository
    {
        /// <summary>
        /// Add history to the database
        /// </summary>
        Task AddAsync(History history);

        /// <summary>
        /// Remove history from the database
        /// </summary>
        Task RemoveAsync(History history);

        /// <summary>
        /// Get history by id
        /// </summary>
        Task<History> GetByIdAsync(int id);

        /// <summary>
        /// Getting all histories from the database
        /// </summary>
        Task<List<History>> GetAllHistories();

        /// <summary>
        /// Get history of user's wallet
        /// </summary>
        Task<List<History>> GetHistoriesOfWalletAsync(Wallet wallet);

        /// <summary>
        /// Get history of wallet by it's id
        /// </summary>
        Task<List<History>> GetHistoriesByWalletIdAsync(int walletId);
    }
}
