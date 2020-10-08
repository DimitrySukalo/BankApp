namespace BankApp.DAL.Entities
{
    public class PiggyBank
    {
        /// <summary>
        /// Piggy bank id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Money in the piggy bank
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// User who live in this country
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Type of currency of piggy bank 
        /// </summary>
        public Currencies Currency { get; set; }
    }
}
