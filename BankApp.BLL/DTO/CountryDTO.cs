using BankApp.DAL.Entities;

namespace BankApp.BLL.DTO
{
    /// <summary>
    /// Country dto
    /// </summary>
    public class CountryDTO
    {
        /// <summary>
        /// Contry name
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Name of the city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// User who live in this country
        /// </summary>
        public User User { get; set; }
    }
}
