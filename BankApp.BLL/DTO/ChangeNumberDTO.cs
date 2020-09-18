using BankApp.DAL.Entities;

namespace BankApp.BLL.DTO
{
    /// <summary>
    /// Change number dto
    /// </summary>
    public class ChangeNumberDTO
    {
        /// <summary>
        /// User new phone number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Current user
        /// </summary>
        public User User { get; set; }
    }
}
