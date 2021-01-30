using Api.Core.Domain;
using Api.Core.Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.ModelConfiguration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.Property(x => x.Operation)
                .HasConversion(
                    operation => operation.Id,
                    str => Operation.FromDisplayName<Operation>(str));

            builder.OwnsOne(x => x.UnitPrice, b =>
            {
                b.Property(x => x.Value)
                    .HasPrecision(15, 4);

                b.Property(x => x.Currency)
                    .HasMaxLength(3)
                    .HasConversion(
                        currency => currency.Value,
                        str => (Currency)str);
            });
        }
    }
}
