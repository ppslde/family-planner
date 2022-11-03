using FamilyPlaner.Domain.Entities.ShopList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyPlaner.Infrastructure.Persistence.Configurations.ShopList;

internal class ShopListItemConfiguration : EntityConfiguration<ShopListItem>
{
    public override void Configure(EntityTypeBuilder<ShopListItem> builder)
    {
        builder.Property(l => l.Quantity)
            .HasDefaultValue(1);

        base.Configure(builder);
    }
}
