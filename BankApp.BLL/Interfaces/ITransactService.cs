using BankApp.BLL.DTO;
using BankApp.DAL.Interfaces;
using BankApp.DAL.Models;
using System.Threading.Tasks;

namespace BankApp.BLL.Interfaces
{
    /// <summary>
    /// Transact service
    /// </summary>
    public interface ITransactService
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Transact money between wallets
        /// </summary>
        Task<OperationSuccessed> TransactMoney(WalletDTO fromWalletDTO, WalletDTO toWalletDTO, decimal transactSum);
    }
}
