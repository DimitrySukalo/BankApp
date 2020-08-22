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
        public DbSet<Country> Countries { get; set; }

        /// <summary>
        /// Creating database
        /// </summary>
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}