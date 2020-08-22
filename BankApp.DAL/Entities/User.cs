using BankApp.DAL.Models;
using Microsoft.AspNetCore.Identity;

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
    }
}
