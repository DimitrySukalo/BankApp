namespace BankApp.DAL.Entities
{
    /// <summary>
    /// User question
    /// </summary>
    public class UserMessage
    {
        /// <summary>
        /// Message id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// UserEmail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User message
        /// </summary>
        public string Message { get; set; }
    }
}
