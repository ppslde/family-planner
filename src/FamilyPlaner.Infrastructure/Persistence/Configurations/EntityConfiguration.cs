using FamilyPlaner.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyPlaner.Infrastructure.Persistence.Configurations;

public abstract class EntityConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : Entity
{
    public virtual void Configure(EntityTypeBuilder<TBase> builder)
    {
        builder.ToTable(typeof(TBase).Name);

        builder.HasQueryFilter(t => !t.Deleted);

        builder
          .Property(t => t.CreatedBy)
          .HasMaxLength(64)
          .IsRequired();

        builder
          .Property(t => t.LastModifiedBy)
          .HasMaxLength(64)
          .IsRequired();
    }
}
