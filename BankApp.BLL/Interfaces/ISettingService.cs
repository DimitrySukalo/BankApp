using BankApp.BLL.DTO;
using BankApp.DAL.Interfaces;
using BankApp.DAL.Models;
using System.Threading.Tasks;

namespace BankApp.BLL.Interfaces
{
    /// <summary>
    /// Setting profile service
    /// </summary>
    public interface ISettingService
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        IUnitOfWork unitOfWork { get; }

        /// <summary>
        /// Change country
        /// </summary>
        Task<OperationSuccessed> ChangeCountryAsync(CountryDTO countryDTO);

        /// <summary>
        /// Change number
        /// </summary>
        Task<OperationSuccessed> ChangeNumberAsync(ChangeNumberDTO changeNumberDTO);
    }
}
