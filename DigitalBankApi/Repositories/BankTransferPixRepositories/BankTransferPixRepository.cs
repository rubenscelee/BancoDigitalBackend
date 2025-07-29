using DigitalBankApi.DTOs;
using DigitalBankApi.Models;
using DigitalBankApi.Repositories.BanckAccountRepositories;
using DigitalBankApi.ResultModels;
using DigitalBankApi.Context;
using DigitalBankApi.Repositories.PixRespositories;
using DigitalBankApi.Repositories.UserRepositories;

namespace DigitalBankApi.Repositories.BankTransferPixRepositories
{
    public class BankTransferPixRepository : IBankTransferPixRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPixRepository _pixRepository;
        private readonly IUserRepository _iUserRepository;
        private readonly ILogger _logger;
        private readonly IBanckAccountRepository _banckAccountRepository;

        public BankTransferPixRepository(ApplicationDbContext context, IPixRepository pixRepository, IUserRepository userRepository, ILogger<BankTransferPixRepository> logger, IBanckAccountRepository banckAccountRepository)
        {
            _context = context;
            _pixRepository = pixRepository;
            _iUserRepository = userRepository;
            _logger = logger;
            _banckAccountRepository = banckAccountRepository;
        }

        /// <summary>
        /// Method used to list Pix tranfers
        /// Using IEnumerable because it's readonly
        /// </summary>
        /// <param name="emailUser"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BankTransferPix>> ListTransferPix(string emailUser)
        {
            var user = await _iUserRepository.GetUserByEmail(emailUser);

            return _context.BankTransferPix.Where(b => b.BankAccountSender.UserId == user.Id);
        }

        /// <summary>
        /// Method used to make Transfers by Pix
        /// </summary>
        /// <param name="bankTransferPix"></param>
        /// <returns></returns>
        public async Task<TransferPixResult> MakeTransferPix(BankTransferPixDto bankTransferPix) {

            var transferAmount = bankTransferPix.TransferAmount;

            _logger.LogInformation("Buscando pix: {0}", bankTransferPix.PixKey);

            var pix = await _pixRepository.GetPixByKey(bankTransferPix.PixKey);

            if (pix is null) return new TransferPixResult() { Success = false, Message = "Key Pix not Found" };

            _logger.LogInformation("Buscando conta do recebedor: {0}", pix.BankAccount.Id);

            var bankAccountReceiver = pix.BankAccount;

            _logger.LogInformation("Buscando usuário com o email informado: {0}", bankTransferPix.EmailSender);

            var userSender = await _iUserRepository.GetUserByEmail(bankTransferPix.EmailSender);

            if(userSender is null)
                return new TransferPixResult() { Success = false, Message = "User Sender not found." };

            var bankAccountSender = userSender?.BankAccount;

            if(bankAccountSender is null)
                return new TransferPixResult() { Success = false, Message = "Bank Account sender not found." };

            return await _banckAccountRepository.UpdateBalanceTransferPixBankAccount(bankTransferPix, bankAccountSender, bankAccountReceiver, transferAmount);
        }
    }
}
