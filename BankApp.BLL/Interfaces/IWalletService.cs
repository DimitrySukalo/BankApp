﻿using BankApp.BLL.DTO;
using BankApp.DAL.Interfaces;
using System.Threading.Tasks;

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

        /// <summary>
        /// Add wallet to the database
        /// </summary>
        Task AddWalletAsync(WalletDTO walletDTO);
    }
}
