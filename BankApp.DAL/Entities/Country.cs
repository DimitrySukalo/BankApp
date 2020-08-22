using BankApp.DAL.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.DAL.Models
{
    /// <summary>
    /// Contry where live our user
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Country id in database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Contry name
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Name of the city
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// User foreign key
        /// </summary>
        [ForeignKey("User")]
        public string UserForeignKey { get; set; }

        /// <summary>
        /// User who live in this country
        /// </summary>
        public User User { get; set; }
    }
}
