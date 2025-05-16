using DigitalBankApi.DTOs;
using DigitalBankApi.Models;

namespace DigitalBankApi.ResultModels
{
    public class TransferPixResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public BankTransferPixDto? BankTransferPixDto { get; set; }
    }
}
