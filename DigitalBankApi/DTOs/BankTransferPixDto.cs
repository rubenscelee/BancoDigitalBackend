namespace DigitalBankApi.DTOs
{
    public record BankTransferPixDto(
        string Title,
        string Description,
        decimal TransferAmount,
        string PixKey
    );
}
