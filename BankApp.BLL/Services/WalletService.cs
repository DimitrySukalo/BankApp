using AutoMapper;
using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.DAL.Interfaces;
using BankApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.BLL.Services
{
    public class WalletService : IWalletService
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Initialization
        /// </summary>
        public WalletService(IUnitOfWork unitOfWork)
        {
            if(unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork), " was null.");
            }

            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Add wallet to the database
        /// </summary>
        public async Task<OperationSuccessed> AddWalletAsync(WalletDTO walletDTO)
        {
            if(walletDTO != null)
            {
                //Mapper configuration
                var configuration = new MapperConfiguration(conf => conf.CreateMap<WalletDTO, Wallet>());

                //Mapper
                var mapper = new Mapper(configuration);

                //Creating user message
                var wallet = mapper.Map<WalletDTO, Wallet>(walletDTO);
                string walletNumber = await GetWalletNumber();

                wallet.Number = walletNumber;

                //Adding wallet to the database
                await UnitOfWork.WalletRepository.AddWalletAsync(wallet);

                var historyOfCreating = new History()
                {
                    Created = DateTime.Now,
                    HistoryType = HistoryType.Creating,
                    Message = "Wallet have been created",
                    Wallet = wallet
                };

                await UnitOfWork.HistoryRepository.AddAsync(historyOfCreating);

                //Saving the database
                await UnitOfWork.SaveAsync();

                return new OperationSuccessed("Wallet is added", true);
            }
            else
            {
                return new OperationSuccessed("Wallet is not added", false);
            }
        }

        private async Task<string> GetWalletNumber()
        {
            var rnd = new Random();

            var strNumber1 = rnd.Next(1000, 9999).ToString();
            var strNumber2 = rnd.Next(1000, 9999).ToString();
            var strNumber3 = rnd.Next(1000, 9999).ToString();
            var strNumber4 = rnd.Next(1000, 9999).ToString();

            var walletNumber = strNumber1 + strNumber2 + strNumber3 + strNumber4;
            var allDbWallets = await UnitOfWork.Database.Wallets.ToListAsync();

            foreach(var wallet in allDbWallets)
            {
                if(wallet.Number == walletNumber)
                {
                    await GetWalletNumber();
                }
            }

            return walletNumber;
        }

        /// <summary>
        /// Removing wallet from the database
        /// </summary>
        public async Task<OperationSuccessed> DeleteWalletByIdAsync(int id)
        {
            var wallet = await UnitOfWork.WalletRepository.GetWalletByIdAsync(id);
            if(wallet != null)
            {
                var user = wallet.User;
                var userInDb = await UnitOfWork.Database.Users.Include(u => u.PiggyBanks).FirstOrDefaultAsync(u => u.Id == user.Id);
                if(userInDb != null)
                {
                    var piggyBank = userInDb.PiggyBanks.FirstOrDefault(pb => pb.Currency == wallet.Currency);
                    piggyBank.Money += wallet.Money;
                }
            }
            await UnitOfWork.WalletRepository.RemoveWalletByIdAsync(id);
            return new OperationSuccessed("Wallet is removed", true);
        }
    }
}
