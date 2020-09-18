using BankApp.DAL.Entities;

namespace BankApp.BLL.DTO
{
    /// <summary>
    /// Wallet
    /// </summary>
    public class WalletDTO
    {
        /// <summary>
        /// Wallet number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Wallet code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Currencies
        /// </summary>
        public Currencies Currency { get; set; }

        /// <summary>
        /// User who have this wallet
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Money in the wallet
        /// </summary>
        public decimal Money { get; set; }
    }
}
