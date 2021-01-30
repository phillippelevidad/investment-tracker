using Api.Core.Domain;
using Api.Core.Domain.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Transactions;

namespace Api.Data.ModelConfiguration
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.ToTable("Assets");

            builder.Property(x => x.Name)
                .HasMaxLength(30);

            builder.Property(x => x.Broker)
                .HasMaxLength(30);

            builder.Property(x => x.Category)
                .HasMaxLength(30);

            builder.Property(x => x.Currency)
                .HasMaxLength(3)
                .HasConversion(
                    currency => currency.Value,
                    str => (Currency)str);
        }
    }
}
