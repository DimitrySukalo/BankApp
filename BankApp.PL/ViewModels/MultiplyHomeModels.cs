using BankApp.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace BankApp.PL.ViewModels
{
    /// <summary>
    /// Multiply view models
    /// </summary>
    public class MultiplyHomeModels
    {
        /// <summary>
        /// User
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Message view model
        /// </summary>
        [Required(ErrorMessage = "Message form is not fulled")]
        public UserMessageViewModel MessageViewModel { get; set; }
    }
}
