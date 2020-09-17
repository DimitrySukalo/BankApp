using BankApp.BLL.Interfaces;
using BankApp.DAL.Interfaces;
using System;

namespace BankApp.BLL.Services
{
    public class WalletService : IWalletService
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Initialization
        /// </summary>
        public WalletService(IUnitOfWork unitOfWork)
        {
            if(unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork), " was null.");
            }

            UnitOfWork = unitOfWork;
        }
    }
}
