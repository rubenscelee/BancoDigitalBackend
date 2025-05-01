namespace DigitalBankApi.Models
{
    public class BankTransferPix
    {
        public Guid Id { get; set; } = new Guid();
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal TransferAmount { get; set; }
        public Guid BankAccountSenderId { get; set; }
        public Guid BankAccountReceiverId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? PixKey { get; set; }

        // Navigation Properties
        public BankAccount? BankAccountSender { get; set; }
        public BankAccount? BankAccountReceiver { get; set; }
    }
}
