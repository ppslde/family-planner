using FamilyPlaner.Domain.Entities.ShopList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyPlaner.Infrastructure.Persistence.Configurations.ShopList;

internal class StoreConfiguration : EntityConfiguration<Store>
{
    public override void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.Property(l => l.Name)
            .IsRequired()
            .HasMaxLength(64);

        base.Configure(builder);
    }
}

internal class StoreCategoryConfiguration : EntityConfiguration<StoreCategory>
{
    public override void Configure(EntityTypeBuilder<StoreCategory> builder)
    {
        builder.Property(l => l.Order)
            .HasDefaultValue(1);

        base.Configure(builder);
    }
}
