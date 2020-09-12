using System.ComponentModel.DataAnnotations;

namespace BankApp.PL.ViewModels
{
    /// <summary>
    /// Message view model
    /// </summary>
    public class UserMessageViewModel
    {
        /// <summary>
        /// User first name
        /// </summary>
        [Required(ErrorMessage = "First name is not fulled")]
        public string FirstName { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        [Required(ErrorMessage = "Last name is not fulled")]
        public string LastName { get; set; }

        /// <summary>
        /// UserEmail
        /// </summary>
        [Required(ErrorMessage = "Email is not fulled")]
        public string Email { get; set; }

        /// <summary>
        /// User message
        /// </summary>
        [Required(ErrorMessage = "Message is not inputed")]
        public string Message { get; set; }
    }
}
