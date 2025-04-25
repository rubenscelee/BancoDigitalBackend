using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DigitalBankApi.Context;

namespace DigitalBankApi.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // ✅ Replace this with your actual connection string
            optionsBuilder.UseSqlServer("Server=Localhost;Database=BancoDigitalApi;User Id=SA;Password=98774923Rubens;TrustServerCertificate=True;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
