using BankApp.BLL.DTO;
using BankApp.DAL.Entities;
using BankApp.DAL.Interfaces;
using BankApp.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp.BLL.Interfaces
{
    /// <summary>
    /// History service
    /// </summary>
    public interface IHistoryService
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// Get all histories
        /// </summary>
        /// <returns></returns>
        Task<List<History>> GeAllHistories();

        /// <summary>
        /// Get histories of wallet
        /// </summary>
        Task<List<History>> GetHistoriesOfWallet(int walletId);
    }
}
