using System;

namespace BankApp.DAL.Entities
{
    /// <summary>
    /// Wallet history
    /// </summary>
    public class History
    {
        /// <summary>
        /// History id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// History message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// History creating
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Wallet id who have this history
        /// </summary>
        public int WalletId { get; set; }

        /// <summary>
        /// Collection of wallets
        /// </summary>
        public Wallet Wallet { get; set; }

        /// <summary>
        /// History type
        /// </summary>
        public HistoryType HistoryType { get; set; }
    }
}
