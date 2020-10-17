using AutoMapper;
using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.DAL.Interfaces;
using BankApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BankApp.BLL.Services
{
    /// <summary>
    /// Implementation of transact service
    /// </summary>
    public class TransactService : ITransactService
    {
        public IUnitOfWork UnitOfWork { get; }

        public TransactService(IUnitOfWork unitOfWork)
        {
            if(unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork), "was null.");
            }

            UnitOfWork = unitOfWork;
        }

        public async Task<OperationSuccessed> TransactMoney(WalletDTO fromWalletDTO, WalletDTO toWalletDTO, decimal transactSum)
        {
            if(fromWalletDTO != null && toWalletDTO != null)
            {
                //Mapper configuration
                var configuration = new MapperConfiguration(conf => conf.CreateMap<WalletDTO, Wallet>());

                //Mapper
                var mapper = new Mapper(configuration);

                //Creating wallets
                var fromWallet = mapper.Map<WalletDTO, Wallet>(fromWalletDTO);
                var toWallet = mapper.Map<WalletDTO, Wallet>(toWalletDTO);

                var walletFromInDb = await UnitOfWork.Database.Wallets.Include(w => w.Histories).FirstOrDefaultAsync(w => w.Number == fromWallet.Number);
                var walletToInDb = await UnitOfWork.Database.Wallets.Include(w => w.Histories).FirstOrDefaultAsync(w => w.Number == toWallet.Number); ;

                if(walletFromInDb != null && walletToInDb != null)
                {
                    if(fromWallet.Currency == toWallet.Currency)
                    {
                        if (transactSum > 0 && transactSum <= walletFromInDb.Money)
                        {
                            walletFromInDb.Money -= transactSum;
                            walletToInDb.Money += transactSum;

                            var historyTransact = new History()
                            {
                                Created = DateTime.Now,
                                HistoryType = HistoryType.Transaction,
                                Message = "Transaction completed successfully."
                            };

                            var historyWithdrawing = new History()
                            {
                                Created = DateTime.Now,
                                HistoryType = HistoryType.Widthdraw,
                                Message = $"From your wallet №{fromWallet.Number} was withdrawed {transactSum}."
                            };

                            var historyAddMoney = new History()
                            {
                                Created = DateTime.Now,
                                HistoryType = HistoryType.AddMoney,
                                Message = $"To your wallet №{toWallet.Number} was added {transactSum}."
                            };

                            walletFromInDb.Histories.Add(historyTransact);
                            walletFromInDb.Histories.Add(historyWithdrawing);
                            walletToInDb.Histories.Add(historyAddMoney);

                            await UnitOfWork.SaveAsync();

                            return new OperationSuccessed("Transaction completed successfully.", true);
                        }
                        else
                        {
                            return new OperationSuccessed("Transaction sum is not correct.", false);
                        }
                    }
                    else
                    {
                        return new OperationSuccessed("Wallets currencies is not equals.", false);
                    }
                }
                else
                {
                    return new OperationSuccessed("Wallets doesn't exist.", false);
                }
            }
            else
            {
                return new OperationSuccessed("Data is not correct", false);
            }
        }
    }
}
