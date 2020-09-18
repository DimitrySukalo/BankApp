﻿using BankApp.DAL.Entities;
using BankApp.DAL.Entity;
using BankApp.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BankApp.DAL.Repositories
{
    /// <summary>
    /// Implementation of IUnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Sign in manager
        /// </summary>
        public SignInManager<User> SignInManager { get; }

        /// <summary>
        /// User manager
        /// </summary>
        public UserManager<User> UserManager { get; }

        /// <summary>
        /// Database
        /// </summary>
        public BankContext Database { get; }

        /// <summary>
        /// Wallet repository
        /// </summary>
        private WalletRepository walletRepository;

        /// <summary>
        /// Country repository
        /// </summary>
        private CountryRepository countryRepository;

        /// <summary>
        /// Initialization
        /// </summary>
        public UnitOfWork(UserManager<User> userManager, SignInManager<User> signInManager, BankContext context)
        {
            if(userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager), " was null.");
            }

            if(signInManager == null)
            {
                throw new ArgumentNullException(nameof(signInManager), " was null.");
            }

            if(context == null)
            {
                throw new ArgumentNullException(nameof(context), " was null.");
            }

            UserManager = userManager;
            SignInManager = signInManager;
            Database = context;
        }

        /// <summary>
        /// Wallet repository
        /// </summary>
        public IWalletRepository WalletRepository
        {
            get
            {
                if(walletRepository == null)
                {
                    walletRepository = new WalletRepository(Database);
                }

                return walletRepository;
            }
        }

        /// <summary>
        /// Country repository
        /// </summary>
        public ICountryRepository CountryRepository
        {
            get
            {
                if(countryRepository == null)
                {
                    countryRepository = new CountryRepository(Database);
                }

                return countryRepository;
            }
        }

        /// <summary>
        /// Saving changes in the database
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await Database.SaveChangesAsync();
        }
    }
}
