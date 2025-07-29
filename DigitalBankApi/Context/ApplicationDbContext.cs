using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Reflection.Emit;
using DigitalBankApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalBankApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var bankAccountId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            builder.Entity<User>()
                .ToTable("UserApi")
                .HasOne(u => u.BankAccount);

            builder.Entity<BankAccount>()
               .ToTable("BankAccount");


            builder.Entity<Pix>()
               .ToTable("Pix")
               .HasOne(b => b.BankAccount)
               .WithMany(b => b.Pix);

            builder.Entity<BankTransferPix>()
                .HasOne(t => t.BankAccountSender)
                .WithMany(b => b.SentTransfers)
                .HasForeignKey(t => t.BankAccountSenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BankTransferPix>()
                .HasOne(t => t.BankAccountReceiver)
                .WithMany(b => b.ReceivedTransfers)
                .HasForeignKey(t => t.BankAccountReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<User>().HasData(new User
            //{
            //    Id = userId,
            //    Email = "test@example.com",
            //    IsEnabled = true,
            //    CreatedOn = DateTime.UtcNow
            //});

            //builder.Entity<BankAccount>().HasData(new BankAccount
            //{
            //    Id = bankAccountId,
            //    Agencia = "1234",
            //    Conta = "567890",
            //    Banco = "MyBank",
            //    CreatedOn = DateTime.UtcNow,
            //    UserId = userId // ✅ This matches the User's Id
            //});
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Pix> Pix { get; set; }
        public DbSet<BankTransferPix> BankTransferPix { get; set; }

    }
}
