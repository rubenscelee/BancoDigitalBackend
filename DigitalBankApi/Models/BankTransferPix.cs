namespace DigitalBankApi.Models
{
    public class BankTransferPix
    {
        public Guid Id { get; set; } = new Guid();
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string PixKey { get; set; } = String.Empty;
    }
}
