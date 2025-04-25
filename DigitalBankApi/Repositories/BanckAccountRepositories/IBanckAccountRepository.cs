using DigitalBankApi.Models;
using DigitalBankApi.ResultModels;

namespace DigitalBankApi.Repositories.BanckAccountRepositories
{
    public interface IBanckAccountRepository
    {
        CreateBankAccountResult CreateBankAccount(BankAccount banckAccount);
    }
}
