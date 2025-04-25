using DigitalBankApi.Models;

namespace DigitalBankApi.ResultModels
{
    public class CreateUserResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public User? User { get; set; }
    }
}
