using DigitalBankApi.DTOs;
using DigitalBankApi.Models;
using DigitalBankApi.Repositories.BanckAccountRepositories;
using DigitalBankApi.ResultModels;
using DigitalBankApi.Context;
using DigitalBankApi.Repositories.PixRespositories;

namespace DigitalBankApi.Repositories.BankTransferPixRepositories
{
    public class BankTransferPixRepository : IBankTransferPixRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPixRepository _pixRepository;

        public BankTransferPixRepository(ApplicationDbContext context, IPixRepository pixRepository)
        {
            _context = context;
            _pixRepository = pixRepository;
        }

        public async Task<CreateBankTransferPixResult> MakeTransferPix(BankTransferPixDto bankTransferPix) {
            Task<Pix> pix = _pixRepository.GetPixByKey(bankTransferPix.PixKey);

            return new CreateBankTransferPixResult();
        }
       

    }
}
