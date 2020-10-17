using BankApp.DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankApp.PL.ViewModels
{
    public class TransactionViewModel
    {
        /// <summary>
        /// User wallets
        /// </summary>
        public IEnumerable<Wallet> Wallets { get; set; }

        /// <summary>
        /// Wallet's id of user who send money
        /// </summary>
        [Required(ErrorMessage = "Your wallet is not choosed.")]
        public int WalletFromId { get; set; }

        /// <summary>
        /// Wallet's id of user who take money
        /// </summary>

        [Required(ErrorMessage = "Wallet number is not inputed.")]
        public string WalletToNumber { get; set; }

        /// <summary>
        /// Sum of transaction
        /// </summary>
        [Required(ErrorMessage = "Sum of transaction is not inputed.")]
        public decimal SumOfTransaction { get; set; }
    }
}
