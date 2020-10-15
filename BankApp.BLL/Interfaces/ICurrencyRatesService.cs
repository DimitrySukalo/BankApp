using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp.BLL.Interfaces
{
    /// <summary>
    /// Currency rates service
    /// </summary>
    public interface ICurrencyRatesService
    {
        Task<Dictionary<string, List<decimal>>> GetCurrencyRatesBuy();  
    }
}
