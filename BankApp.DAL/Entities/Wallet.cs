using System.Collections.Generic;

namespace BankApp.DAL.Entities
{
    /// <summary>
    /// User wallet
    /// </summary>
    public class Wallet
    {
        /// <summary>
        /// Wallet id
        /// </summary>
        public int Id { get; set; }

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

        /// <summary>
        /// Histories
        /// </summary>
        public virtual ICollection<History> Histories { get; set; }
    }
}
