using BankApp.BLL.DTO;
using BankApp.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BankApp.BLL.Interfaces
{
    /// <summary>
    /// Register service
    /// </summary>
    public interface IRegisterService
    {
        /// <summary>
        /// Register user
        /// </summary>
        IdentityResult RegisterUserAsync(UserDTO userDTO);

        /// <summary>
        /// Unit of work
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}
