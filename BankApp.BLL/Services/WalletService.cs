using AutoMapper;
using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.DAL.Interfaces;
using BankApp.DAL.Models;
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
                var rnd = new Random();

                var strNumber1 = rnd.Next(1000,9999).ToString();
                var strNumber2 = rnd.Next(1000, 9999).ToString();
                var strNumber3 = rnd.Next(1000, 9999).ToString();
                var strNumber4 = rnd.Next(1000, 9999).ToString();

                var walletNumber = strNumber1 + strNumber2 + strNumber3 + strNumber4;
                wallet.Number = walletNumber;

                //Adding wallet to the database
                await UnitOfWork.WalletRepository.AddWalletAsync(wallet);

                //Saving the database
                await UnitOfWork.SaveAsync();

                return new OperationSuccessed("Wallet is added", true);
            }
            else
            {
                return new OperationSuccessed("Wallet is not added", false);
            }
        }
    }
}
