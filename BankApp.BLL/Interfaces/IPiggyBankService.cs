using BankApp.BLL.DTO;
using BankApp.DAL.Interfaces;
using BankApp.DAL.Models;
using System.Threading.Tasks;

namespace BankApp.BLL.Interfaces
{
    /// <summary>
    /// Piggy bank service
    /// </summary>
    public interface IPiggyBankService
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Withdraw money from the pigyy bank
        /// </summary>
        Task<OperationSuccessed> WithdrawMoneyFromPiggyBankAsync(PiggyBankDTO piggyBankDTO);
    }
}
