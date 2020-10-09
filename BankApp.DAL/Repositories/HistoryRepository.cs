using BankApp.DAL.Entities;
using BankApp.DAL.Entity;
using BankApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.DAL.Repositories
{
    /// <summary>
    /// Implementation of the history repository
    /// </summary>
    public class HistoryRepository : IHistoryRepository
    {
        /// <summary>
        /// Database
        /// </summary>
        private readonly BankContext db;

        /// <summary>
        /// Initialization
        /// </summary>
        public HistoryRepository(BankContext context)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context), " was null.");
            }

            db = context;
        }

        /// <summary>
        /// Add history to the database
        /// </summary>
        public async Task AddAsync(History history)
        {
            if(history != null)
            {
                await db.Histories.AddAsync(history);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Getting all histories from the database
        /// </summary>
        public async Task<List<History>> GetAllHistories()
        {
            var histories = await db.Histories.Include(h => h.Wallet).ToListAsync();
            return histories;
        }

        /// <summary>
        /// Get history by id
        /// </summary>
        public async Task<History> GetByIdAsync(int id)
        {
            var history = await db.Histories.Include(h => h.Wallet).FirstOrDefaultAsync(history => history.Id == id);
            return history;
        }

        /// <summary>
        /// Get history of wallet by it's id
        /// </summary>
        public async Task<List<History>> GetHistoriesByWalletIdAsync(int walletId)
        {
            var histories = await db.Histories.Where(h => h.WalletId == walletId).ToListAsync();
            return histories;
        }

        /// <summary>
        /// Get history of user's wallet
        /// </summary>
        public async Task<List<History>> GetHistoriesOfWalletAsync(Wallet wallet)
        {
            var histories = await db.Histories.Where(h => h.WalletId == wallet.Id).ToListAsync();
            return histories;
        }

        /// <summary>
        /// Remove history from the database
        /// </summary>
        public async Task RemoveAsync(History history)
        {
            if(history != null)
            {
                db.Histories.Remove(history);
                await db.SaveChangesAsync();
            }
        }
    }
}
