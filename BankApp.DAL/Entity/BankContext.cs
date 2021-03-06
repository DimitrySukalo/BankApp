﻿using BankApp.DAL.Entities;
using BankApp.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankApp.DAL.Entity
{
    /// <summary>
    /// Database context
    /// </summary>
    public class BankContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Users countries
        /// </summary>
        public virtual DbSet<Country> Countries { get; set; }

        /// <summary>
        /// User messages
        /// </summary>
        public virtual DbSet<UserMessage> Messages { get; set; }

        /// <summary>
        /// Histories
        /// </summary>
        public virtual DbSet<History> Histories { get; set; }

        /// <summary>
        /// Wallets
        /// </summary>
        public virtual DbSet<Wallet> Wallets { get; set; }

        /// <summary>
        /// Piggy banks
        /// </summary>
        public virtual DbSet<PiggyBank> PiggyBanks { get; set; }

        public BankContext()
        {
        }

        /// <summary>
        /// Creating database
        /// </summary>
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
