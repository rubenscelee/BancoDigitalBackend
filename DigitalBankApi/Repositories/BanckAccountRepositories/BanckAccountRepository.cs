using DigitalBankApi.Context;
using DigitalBankApi.Models;
using DigitalBankApi.ResultModels;

namespace DigitalBankApi.Repositories.BanckAccountRepositories
{
    public class BanckAccountRepository : IBanckAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public BanckAccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public CreateBankAccountResult CreateBankAccount(BankAccount banckAccount)
        {
            return new CreateBankAccountResult();
        }
    }
}
