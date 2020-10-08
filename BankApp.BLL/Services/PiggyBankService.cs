using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Interfaces;
using BankApp.DAL.Models;
using System;
using System.Threading.Tasks;

namespace BankApp.BLL.Services
{
    /// <summary>
    /// Implementation of the piggy bank service
    /// </summary>
    public class PiggyBankService : IPiggyBankService
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }

        public PiggyBankService(IUnitOfWork unitOfWork)
        {
            if(unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork), "was null.");
            }

            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Withdraw money from the piggy bank
        /// </summary>
        public async Task<OperationSuccessed> WithdrawMoneyFromPiggyBankAsync(PiggyBankDTO piggyBankDTO)
        {
            if(piggyBankDTO != null)
            {
                var money = piggyBankDTO.PiggyBank.Money;

                if (piggyBankDTO.WithdrawSum <= money && piggyBankDTO.WithdrawSum > 0)
                {
                    var wallet = await UnitOfWork.WalletRepository.GetByNumberAsync(piggyBankDTO.CardNumber);
                    if(wallet != null && wallet.Currency == piggyBankDTO.PiggyBank.Currency)
                    {
                        wallet.Money += piggyBankDTO.WithdrawSum;
                        piggyBankDTO.PiggyBank.Money -= piggyBankDTO.WithdrawSum;

                        await UnitOfWork.SaveAsync();
                        return new OperationSuccessed("Transaction is made.", true);
                    }
                    else
                    {
                        return new OperationSuccessed("Wallet doesn't correct.", false);
                    }
                }
                else
                {
                    return new OperationSuccessed("There are not enough funds in your account", false);
                }
            }
            else
            {
                return new OperationSuccessed("Incorrect converting", false);
            }
        }
    }
}
