using Api.Core.Domain.Assets;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class InvestmentDbContext : DbContext
    {
        public InvestmentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
    }
}
