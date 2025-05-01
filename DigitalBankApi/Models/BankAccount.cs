namespace DigitalBankApi.Models
{
    public class BankAccount
    {
        public Guid Id { get; set; } = new Guid();
        public string? Agencia { get; set; }
        public string? Conta { get; set; }
        public string? Banco { get; set; }
        public decimal Saldo { get; set; } = 0;
        public Guid? UserId { get; set; } // Foreign Key
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public ICollection<Pix> Pix { get; set; } = new HashSet<Pix>();
        public ICollection<BankTransferPix> SentTransfers { get; set; } = new List<BankTransferPix>();
        public ICollection<BankTransferPix> ReceivedTransfers { get; set; } = new List<BankTransferPix>();
    }
}
