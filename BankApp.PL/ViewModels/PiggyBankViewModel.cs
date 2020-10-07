using BankApp.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace BankApp.PL.ViewModels
{
    /// <summary>
    /// Piggy bank view model
    /// </summary>
    public class PiggyBankViewModel
    {
        /// <summary>
        /// Current user in the session
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Sum which user want to withdraw
        /// </summary>
        [Required(ErrorMessage = "Sum is not inputed.")]
        public decimal WithdrawSum { get; set; }

        /// <summary>
        /// User's card number
        /// </summary>
        [Required(ErrorMessage = "Card number is not inputed.")]
        public decimal CardNumber { get; set; }
    }
}
