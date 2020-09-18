using BankApp.DAL.Entities;
using System.Collections.Generic;

namespace BankApp.PL.ViewModels
{
    /// <summary>
    /// Profile view model
    /// </summary>
    public class ProfileViewModel
    {
        /// <summary>
        /// User
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Wallets
        /// </summary>
        public IEnumerable<Wallet> Wallets { get; set; }
    }
}
