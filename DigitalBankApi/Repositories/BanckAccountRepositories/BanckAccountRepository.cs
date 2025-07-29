using DigitalBankApi.Context;
using DigitalBankApi.DTOs;
using DigitalBankApi.Models;
using DigitalBankApi.ResultModels;
using Microsoft.EntityFrameworkCore;

namespace DigitalBankApi.Repositories.BanckAccountRepositories
{
    public class BanckAccountRepository : IBanckAccountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public BanckAccountRepository(ApplicationDbContext context, ILogger<BanckAccountRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public CreateBankAccountResult CreateBankAccount(BankAccount banckAccount)
        {
            return new CreateBankAccountResult();
        }

        public Task<bool> UpdateBankAccount(Guid bankAccountId)
        {
            throw new NotImplementedException();
        }

        public async Task<TransferPixResult> UpdateBalanceTransferPixBankAccount(BankTransferPixDto bankTransferPixDto, BankAccount banckAccountSender, BankAccount banckAccountReceiver, decimal transferAmount)
        {
            if (banckAccountSender.Saldo < transferAmount)
                return new TransferPixResult() { Success = false, Message = "Insufficient funds in the sender's account." };

            await using var transaction = await _context.Database.BeginTransactionAsync();

            _logger.LogError("Creating SavePoint: BeforePixTransfer");

            transaction.CreateSavepoint("BeforePixTransfer");

            try
            {
                var saldoSender = banckAccountSender.Saldo;
                var saldoReceiver = banckAccountReceiver.Saldo;

                saldoSender -= transferAmount;
                saldoReceiver += transferAmount;

                banckAccountSender.Saldo = saldoSender;
                banckAccountReceiver.Saldo = saldoReceiver;

                _logger.LogError("Updating Bank Account balance");

                _context.BankAccounts.Update(banckAccountSender);
                _context.BankAccounts.Update(banckAccountReceiver);


                BankTransferPix bankTransferPix = new BankTransferPix() {
                   Title = bankTransferPixDto.Title,
                   Description = bankTransferPixDto.Description,
                   TransferAmount = bankTransferPixDto.TransferAmount,
                   BankAccountSenderId = banckAccountSender.Id,
                   BankAccountReceiverId = banckAccountReceiver.Id,
                   CreatedOn = DateTime.UtcNow,
                   PixKey = bankTransferPixDto.PixKey,
                   BankAccountSender = banckAccountSender,
                   BankAccountReceiver = banckAccountReceiver,
                };

                _logger.LogError("Creating BankTransferPix object");

                await _context.BankTransferPix.AddAsync(bankTransferPix);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                _logger.LogError("The transfer completed successfully.");

                return new TransferPixResult() { Success = true, Message = "The transfer completed successfully." };
            }
            catch (Exception ex) {
                await transaction.RollbackToSavepointAsync("BeforePixTransfer");

                _logger.LogError(ex, "An error occurred during Pix transfer for accounts {SenderId} and {ReceiverId}", banckAccountSender.Id, banckAccountReceiver.Id);

                return new TransferPixResult() { Success = false, Message = "An error occured: " + ex.Message };
            }
        }
    }
}
