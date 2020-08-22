using System.ComponentModel.DataAnnotations;

namespace BankApp.PL.ViewModels
{
    public class RegisterViewModel
    {
        /// <summary>
        /// User Email
        /// </summary>
        [Required(ErrorMessage = "Email is not field")]
        public string Email { get; set; }
        
        /// <summary>
        /// User First name
        /// </summary>
        [Required(ErrorMessage = "First name is not field")]
        public string FirstName { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        [Required(ErrorMessage = "Last name is not field")]
        public string LastName { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        [Required(ErrorMessage = "Password in not field")]
        public string Password { get; set; }

        /// <summary>
        /// Confirmed password
        /// </summary>
        [Compare("Password", ErrorMessage = "Passwords in not equal")]
        public string PasswordConfirm { get; set; }
    }
}
