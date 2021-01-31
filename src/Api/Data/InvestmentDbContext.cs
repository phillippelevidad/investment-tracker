using Core.Domain.Assets;
using Core.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class InvestmentDbContext : DbContext
    {
        public InvestmentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InvestmentDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
