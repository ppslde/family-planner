using FamilyPlaner.Domain.Entities.ShopList;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyPlaner.Infrastructure.Persistence.Configurations.ShopList;

internal class CategoryConfiguration : EntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(l => l.Name)
            .IsRequired()
            .HasMaxLength(64);

        base.Configure(builder);
    }
}
