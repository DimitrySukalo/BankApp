using BankApp.DAL.Models;

namespace BankApp.PL.ViewModels
{
    /// <summary>
    /// Change data view model
    /// </summary>
    public class ChangeDataViewModel
    {
        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// CountyDTO
        /// </summary>
        public Country Country { get; set; }
    }
}
