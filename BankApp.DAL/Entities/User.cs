using BankApp.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.DAL.Entities
{
    /// <summary>
    /// User
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User country
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// User wallets
        /// </summary>
        public virtual ICollection<Wallet> Wallets { get; set; }

        /// <summary>
        /// Piggy bank
        /// </summary>
        public PiggyBank PiggyBank { get; set; }
    }
}
