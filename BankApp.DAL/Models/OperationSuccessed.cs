using System;

namespace BankApp.DAL.Models
{
    public class OperationSuccessed
    {
        /// <summary>
        /// Message about operation
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// If successed operation
        /// </summary>
        public bool Successed { get; set; }

        /// <summary>
        /// Initialization
        /// </summary>
        public OperationSuccessed(string message, bool successed)
        {
            if(string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message), " was null.");
            }

            Message = message;
            Successed = successed;
        }
    }
}
