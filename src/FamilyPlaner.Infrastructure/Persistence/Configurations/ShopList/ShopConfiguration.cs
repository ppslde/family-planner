using FamilyPlaner.Domain.Entities.ShopList;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyPlaner.Infrastructure.Persistence.Configurations.ShopList;

internal class ShopConfiguration : EntityConfiguration<Shop>
{
    public override void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.Property(l => l.Name)
            .IsRequired()
            .HasMaxLength(64);

        base.Configure(builder);
    }
}
