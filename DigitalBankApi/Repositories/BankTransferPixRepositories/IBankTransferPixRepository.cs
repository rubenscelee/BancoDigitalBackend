using DigitalBankApi.DTOs;
using DigitalBankApi.Models;
using DigitalBankApi.ResultModels;

namespace DigitalBankApi.Repositories.BankTransferPixRepositories
{
    public interface IBankTransferPixRepository
    {
        Task<TransferPixResult> MakeTransferPix(BankTransferPixDto bankTransferPix);
        Task<IEnumerable<BankTransferPix>> ListTransferPix(string emailUser);
    }
}
