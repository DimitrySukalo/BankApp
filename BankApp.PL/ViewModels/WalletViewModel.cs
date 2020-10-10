using BankApp.DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankApp.PL.ViewModels
{
    /// <summary>
    /// Wallet view model
    /// </summary>
    public class WalletViewModel
    {
        /// <summary>
        /// Number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        [Required(ErrorMessage = "Code is not filled")]
        public string Code { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public List<Currencies> Currencies = new List<Currencies>()
        {
            DAL.Entities.Currencies.EUR,
            DAL.Entities.Currencies.RUB,
            DAL.Entities.Currencies.UAH,
            DAL.Entities.Currencies.USD
        };

        /// <summary>
        /// Currency
        /// </summary>
        [Required(ErrorMessage = "Currency is not selected")]
        public Currencies Currency { get; set; }

        /// <summary>
        /// User wallets
        /// </summary>
        public IEnumerable<Wallet> Wallets { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Money
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// Histories of wallet
        /// </summary>
        public IEnumerable<History> Histories { get; set; }
    }
}
