namespace DigitalBankApi.Models
{
    public class Pix
    {
        public Guid Id { get; set; } = new Guid();
        public string? ChavePix { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        // Foreign Key
        public Guid BankAccountId { get; set; } // Foreign Key

        public BankAccount BankAccount { get; set; } = null!;
    }
}
