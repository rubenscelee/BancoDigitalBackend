using DigitalBankApi.Models;

namespace DigitalBankApi.ResultModels
{
    public class CreateBankTransferPixResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public BankTransferPix? BankTransferPix { get; set; }
    }
}
