using DigitalBankApi.DTOs;
using DigitalBankApi.Models;
using DigitalBankApi.ResultModels;

namespace DigitalBankApi.Repositories.BanckAccountRepositories
{
    public interface IBanckAccountRepository
    {
        CreateBankAccountResult CreateBankAccount(BankAccount banckAccount);
        Task<bool> UpdateBankAccount(Guid bankAccountId);
        Task<TransferPixResult> UpdateBalanceTransferPixBankAccount(BankTransferPixDto bankTransferPixDto,BankAccount banckAccountSender, BankAccount banckAccountReceiver, decimal transferAmount);
    }
}
