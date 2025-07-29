namespace DigitalBankApi.DTOs
{
    public record BankTransferPixDto(
        string EmailSender,
        string Title,
        string Description,
        decimal TransferAmount,
        string PixKey
    );
}
