using DigitalBankApi.Models;

namespace DigitalBankApi.ResultModels
{
    public class CreateBankAccountResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public BankAccount? BankAccount { get; set; }
    }
}
