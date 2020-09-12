using System.ComponentModel.DataAnnotations;

namespace BankApp.PL.ViewModels
{
    /// <summary>
    /// Login view model
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// User email
        /// </summary>
        [Required(ErrorMessage = "Email is not filled")]
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required(ErrorMessage = "Password is not filled")]
        public string Password { get; set; }
    }
}
