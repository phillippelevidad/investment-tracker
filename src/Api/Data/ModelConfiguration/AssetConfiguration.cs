using Core.Domain;
using Core.Domain.Assets;
using Core.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder
                .HasMany(x => x.Transactions)
                .WithOne(x => x.Asset)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.Property(x => x.Name)
                .HasMaxLength(30);
        }
    }
}
