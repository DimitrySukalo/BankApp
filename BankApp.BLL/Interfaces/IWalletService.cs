using BankApp.DAL.Interfaces;

namespace BankApp.BLL.Interfaces
{
    /// <summary>
    /// Wallet service
    /// </summary>
    public interface IWalletService
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}
