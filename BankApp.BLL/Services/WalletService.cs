using AutoMapper;
using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.DAL.Interfaces;
using BankApp.DAL.Models;
using System;
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
