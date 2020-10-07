using BankApp.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp.DAL.Interfaces
{
    public interface IPiggyBankRepository
    {
        /// <summary>
        /// Add piggy bank to the database
        /// </summary>
        Task AddAsync(PiggyBank piggyBank);

        /// <summary>
        /// Remove piggy bank from the database
        /// </summary>
        Task RemoveAsync(PiggyBank piggyBank);

        /// <summary>
        /// Get all piggy banks
        /// </summary>
        /// <returns></returns>
        Task<List<PiggyBank>> GetAllAsync();

        /// <summary>
        /// Get by id piggy bank
        /// </summary>
        Task<PiggyBank> GetByIdAsync(string id);
    }
}
