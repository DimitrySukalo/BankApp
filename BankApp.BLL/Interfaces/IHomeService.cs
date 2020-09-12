using BankApp.BLL.DTO;
using BankApp.DAL.Models;
using System.Threading.Tasks;

namespace BankApp.BLL.Interfaces
{
    /// <summary>
    /// Home service
    /// </summary>
    public interface IHomeService
    {
        /// <summary>
        /// Save message in the database
        /// </summary>
        Task<OperationSuccessed> SaveUserMessageInDbAsync(UserMessageDTO messageDTO);
    }
}
