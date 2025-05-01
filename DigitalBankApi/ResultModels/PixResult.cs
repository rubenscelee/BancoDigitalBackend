using DigitalBankApi.Models;

namespace DigitalBankApi.ResultModels
{
    public class PixResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public Pix? Pix { get; set; }
    }
}
