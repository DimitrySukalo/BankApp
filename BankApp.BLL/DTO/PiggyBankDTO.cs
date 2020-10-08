using BankApp.DAL.Entities;

namespace BankApp.BLL.DTO
{
    /// <summary>
    /// Piggy bank dto
    /// </summary>
    public class PiggyBankDTO
    {
        /// <summary>
        /// Current user in the session
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Sum which user want to withdraw
        /// </summary>
        public decimal WithdrawSum { get; set; }

        /// <summary>
        /// User's card number
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Piggy bank
        /// </summary>
        public PiggyBank PiggyBank { get; set; }
    }
}
