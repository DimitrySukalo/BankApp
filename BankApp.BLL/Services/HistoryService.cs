using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp.BLL.Services
{
    /// <summary>
    /// Implementation of history service
    /// </summary>
    public class HistoryService : IHistoryService
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }

        public HistoryService(IUnitOfWork unitOfWork)
        {
            if(unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork), " was null.");
            }

            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all histories
        /// </summary>
        public async Task<List<History>> GeAllHistories()
        {
            var histories = await UnitOfWork.HistoryRepository.GetAllHistories();
            return histories;
        }

        /// <summary>
        /// Get histories of wallet
        /// </summary>
        public async Task<List<History>> GetHistoriesOfWallet(int walletId)
        {
            var histories = await UnitOfWork.HistoryRepository.GetHistoriesByWalletIdAsync(walletId);
            return histories;
        }
    }
}
