using System.Threading.Tasks;

namespace BankApp.BLL.Interfaces
{
    /// <summary>
    /// Send email to confirm account
    /// </summary>
    public interface ISendEmailService
    {
        /// <summary>
        /// Send email message
        /// </summary>
        Task SendEmail(string email, string subject, string message);
    }
}
