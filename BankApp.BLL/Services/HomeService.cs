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
        /// Unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initialization
        /// </summary>
        public HomeService(IUnitOfWork unitOfWork)
        {

            if(unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork), " was null.");
            }

            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Save user message in the database
        /// </summary>
        public async Task<OperationSuccessed> SaveUserMessageInDbAsync(UserMessageDTO messageDTO)
        {
            if(messageDTO != null && !string.IsNullOrWhiteSpace(messageDTO.Email) ||
                string.IsNullOrWhiteSpace(messageDTO.FirstName) ||
                string.IsNullOrWhiteSpace(messageDTO.LastName) ||
                string.IsNullOrWhiteSpace(messageDTO.Message))
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
                return new OperationSuccessed("Message dto is NULL", false);
            }
        }
    }
}
