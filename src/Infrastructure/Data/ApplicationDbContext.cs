using System;
using System.Collections.Generic;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BankAccount> bankAccounts { get; set; }

        /* Herencia */
        public DbSet<LineOfCreditAccount> lineOfCreditAccounts { get; set; }
        public DbSet<GiftCardAccount> giftCardAccounts { get; set; }
        public DbSet<InterestEarningAccount> interestEarningAccounts { get; set; }

        public DbSet<Transaction> transactions { get; set; }
        public DbSet<User> Users { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(CreateUserDataSeed());

            base.OnModelCreating(modelBuilder);
        }

        private User[] CreateUserDataSeed()
        {
            User[] result;

            result = [
            new User
             {
                UserName = "admin",
                 Email = "admin@hotmail.com",
                 FirstName = "Admin",
                 LastName = "1",
                 Phone = "123456789",
                 Password="1234",
                 Id = 1
    }];

            return result;
        }
    }
}