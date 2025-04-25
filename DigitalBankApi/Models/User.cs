using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalBankApi.Models
{
    public class User
    {
        public Guid Id { get; set; } = new Guid();
        public string? Email { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;
        public BankAccount? BankAccount { get; set; }
    }
}
