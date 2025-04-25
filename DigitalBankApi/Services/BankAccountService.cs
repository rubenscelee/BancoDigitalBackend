using DigitalBankApi.Models;

namespace DigitalBankApi.Services
{
    public static class BankAccountService
    {
        public static BankAccount CreateBankAccountObject(User user)
        {
            return new BankAccount()
            {
                Agencia = "0001",
                Conta = "12321423",
                Banco = "Nu",
            };
        }
    }
}
