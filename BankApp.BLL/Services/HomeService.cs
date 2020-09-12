using AutoMapper;
using BankApp.BLL.DTO;
using BankApp.BLL.Interfaces;
using BankApp.DAL.Entities;
using BankApp.DAL.Interfaces;
using BankApp.DAL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BankApp.BLL.Services
{
    /// <summary>
    /// Implementation of home service
    /// </summary>
    public class HomeService : IHomeService
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<HomeService> logger;

        /// <summary>
        /// Unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initialization
        /// </summary>
        public HomeService(ILogger<HomeService> logger, IUnitOfWork unitOfWork)
        {
            if(logger == null)
            {
                throw new ArgumentNullException(nameof(logger), " was null.");
            }

            if(unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork), " was null.");
            }

            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Save user message in the database
        /// </summary>
        public async Task<OperationSuccessed> SaveUserMessageInDbAsync(UserMessageDTO messageDTO)
        {
            if(messageDTO != null)
            {
                //Mapper configuration
                var configuration = new MapperConfiguration(conf => conf.CreateMap<UserMessageDTO, UserMessage>());

                //Mapper
                var mapper = new Mapper(configuration);

                //Creating user message
                var userMessage = mapper.Map<UserMessageDTO, UserMessage>(messageDTO);

                //Adding message to the database
                await unitOfWork.Database.Messages.AddAsync(userMessage);

                //Saving database
                await unitOfWork.SaveAsync();

                return new OperationSuccessed("Message have been sended.", true);
            }
            else
            {
                //Displaying information in the console
                logger.LogInformation("Message dto is NULL");
                return new OperationSuccessed("Message dto is NULL", false);
            }
        }
    }
}
