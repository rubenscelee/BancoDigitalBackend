using DigitalBankApi.Models;
using DigitalBankApi.ResultModels;

namespace DigitalBankApi.Repositories.PixRespositories
{
    public interface IPixRepository
    {
        Task<Pix> GetPixByKey(string chavePix);
    }
}
