using BankApp.DAL.Entities;
using BankApp.DAL.Entity;
using BankApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp.DAL.Repositories
{
    public class PiggyBankRepository : IPiggyBankRepository
    {
        /// <summary>
        /// Database
        /// </summary>
        private readonly BankContext db;

        /// <summary>
        /// Inititalization
        /// </summary>
        public PiggyBankRepository(BankContext context)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context), " was null.");
            }

            db = context;
        }

        /// <summary>
        /// Add piggy bank to the database
        /// </summary>
        public async Task AddAsync(PiggyBank piggyBank)
        {
            if(piggyBank != null)
            {
                await db.AddAsync(piggyBank);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get all piggy banks
        /// </summary>
        /// <returns></returns>
        public async Task<List<PiggyBank>> GetAllAsync()
        {
            var piggyBanks = await db.PiggyBanks.Include(b => b.User).ToListAsync();

            return piggyBanks;
        }

        /// <summary>
        /// Get by id piggy bank
        /// </summary>
        public async Task<PiggyBank> GetByIdAsync(int id)
        {
            var piggyBank = await db.PiggyBanks.Include(b => b.User).FirstOrDefaultAsync(b => b.Id == id);

            return piggyBank;
        }

        /// <summary>
        /// Remove piggy bank from the database
        /// </summary>
        public async Task RemoveAsync(PiggyBank piggyBank)
        {
            if(piggyBank != null)
            {
                db.Remove(piggyBank);
                await db.SaveChangesAsync();
            }
        }
    }
}
