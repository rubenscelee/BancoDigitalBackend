using DigitalBankApi.Models;
using DigitalBankApi.ResultModels;
using DigitalBankApi.Context;
using Microsoft.EntityFrameworkCore;

namespace DigitalBankApi.Repositories.PixRespositories
{
    public class PixRepository : IPixRepository
    {
        private readonly ApplicationDbContext _context;

        public PixRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pix> GetPixByKey(string chavePix)
        {
            return await _context.Pix.FirstOrDefaultAsync(p => p.ChavePix.Equals(chavePix));

        }
    }
}
