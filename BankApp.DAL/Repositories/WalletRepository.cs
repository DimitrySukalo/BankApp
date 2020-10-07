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
    /// Implementation of the wallet repository
    /// </summary>
    public class WalletRepository : IWalletRepository
    {
        /// <summary>
        /// Database
        /// </summary>
        private readonly BankContext db;

        /// <summary>
        /// Initialization
        /// </summary>
        public WalletRepository(BankContext context)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context), " was null.");
            }

            db = context;
        }

        /// <summary>
        /// Add wallet to the database
        /// </summary>
        public async Task AddWalletAsync(Wallet wallet)
        {
            if(wallet != null)
            {
                await db.Wallets.AddAsync(wallet);
            }
        }

        /// <summary>
        /// Get all user wallets
        /// </summary>
        public async Task<List<Wallet>> GetAllUserWalletsAync(User user)
        {
            if(user != null)
            {
                //All user wallets from the database
                var wallets = await db.Wallets.Where(w => w.User.Id == user.Id).ToListAsync();
                if(wallets != null)
                {
                    return wallets;
                }
                else
                {
                    return new List<Wallet>();
                }
            }
            else
            {
                return new List<Wallet>();
            }
        }

        /// <summary>
        /// Get all wallet from database
        /// </summary>
        /// <returns></returns>
        public async Task<List<Wallet>> GetAllWalletsAsync()
        {
            //All wallets in the database
            var wallet = await db.Wallets.Include(w => w.User).ToListAsync();
            return wallet;
        }

        /// <summary>
        /// Get wallet by id
        /// </summary>
        public async Task<Wallet> GetWalletByIdAsync(int id)
        {
            //Wallet in the database with the same id
            var wallet = await db.Wallets.Include(w => w.User).FirstOrDefaultAsync(w => w.Id == id);
            return wallet;
        }

        /// <summary>
        /// Remove wallet from the database
        /// </summary>
        public async Task RemoveWalletAsync(Wallet wallet)
        {
            if(wallet != null)
            {
                //Removing wallet
                db.Wallets.Remove(wallet);

                //Saving database
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Remove wallet from the database by id 
        /// </summary>
        public async Task RemoveWalletByIdAsync(int id)
        {
            //Getting wallet from the database with the same id
            var wallet = await db.Wallets.FirstOrDefaultAsync(w => w.Id == id);
            if(wallet != null)
            {
                //Removing wallet from the database
                db.Wallets.Remove(wallet);

                //Saving changes in the database
                await db.SaveChangesAsync();
            }
        }
    }
}
