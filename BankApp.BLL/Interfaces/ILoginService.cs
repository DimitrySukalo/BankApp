using BankApp.BLL.DTO;
using BankApp.DAL.Interfaces;
using BankApp.DAL.Models;

namespace BankApp.BLL.Interfaces
{
    /// <summary>
    /// Login service
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Sign in method
        /// </summary>
        OperationSuccessed SignInUserAccount(UserLoginDTO userLoginDTO);
    }
}
