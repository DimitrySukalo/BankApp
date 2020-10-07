using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.DAL.Entities
{
    /// <summary>
    /// Piggy bank
    /// </summary>
    public class PiggyBank
    {
        /// <summary>
        /// Piggy bank id
        /// </summary>
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }

        /// <summary>
        /// Money in the piggy bank
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// User who live in this country
        /// </summary>
        public User User { get; set; }
    }
}
